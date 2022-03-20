using Xunit;
using FluentAssertions;
using Reduct.System;
using System;
using Xunit.Abstractions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Reduct.Framework.Tests.SystemTests
{
    public class ErrorCodeTests
    {
        private readonly ITestOutputHelper output;
        IConfiguration configuration;

        public ErrorCodeTests(ITestOutputHelper output)
        {
            this.output = output;
            configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        }

        [Theory]
        [InlineData(ErrorCode.SUCCES, 0, "Succesfull")]
        [InlineData(ErrorCode.ERROR_ARGUMENTS_INVALID_AMOUNT, 100, "Invalid amount of arguments provided")]
        [InlineData(ErrorCode.ERROR_ARGUMENT_BAD_INPUT, 101, "Unable to process arguments. Bad arguments")]
        [InlineData(ErrorCode.ERROR_ARGUMENT_ASSEMBLY_NOT_FOUND, 102, "Invalid argument. Input assembly not found")]
        public void ErrorCodes_Messages_Should_Match(ErrorCode input, int expectedInt, string expectedMessage)
        {
            var test = ErrorCodes.GetDescription(input);
            test.ExitCodeInt.Should().Be(expectedInt);
            test.Message.Should().Contain(expectedMessage);
        }

        [Fact]
        public void ErrorCode_Messages_Should_Not_End_With_A_Dot()
        {
            var errorCount = 0;

            foreach (ErrorCode errorCode in Enum.GetValues(typeof(ErrorCode)))
            {
                try
                {
                    var test = ErrorCodes.GetDescription(errorCode);
                    test.Message.Should().NotEndWith(".", "because users should add it themselfs");
                }
                catch (Exception)
                {
                    output.WriteLine("Description for error code [{0}] contains a '.'", errorCode);
                    errorCount++;
                }
            }

            errorCount.Should().Be(0, "no dots are allowed");
        }
    }
}