using Azure.Core;
using FluentAssertions;
using Reduct.Azure;
using Reduct.Framework.Tests.Fixtures;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System;
using System.Threading;
using FluentAssertions.Execution;
using FluentAssertions.Extensions;
using Xunit.Abstractions;
using System.Diagnostics;

namespace Reduct.Framework.Tests.Azure
{

    public class CredentialManagerTests : IClassFixture<DefaultCredentialsFixture>
    {
        private readonly ITestOutputHelper output;
        DefaultCredentialsFixture _fixture;

        public CredentialManagerTests(DefaultCredentialsFixture fixture, ITestOutputHelper output)
        {
            this._fixture = fixture;
            this.output = output;
        }

        [Fact]
        [Trait("feature", "beta")]
        public async void GetToken_Should_Be_Less_Than_750ms_With_Sequential_Calls()
        {
            var credentials = CredentialsManager.GetDefaultCredential(false);

            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

            Action someAction = () => credentials.GetToken(new TokenRequestContext(scopes));

            var sw = Stopwatch.StartNew();
            someAction.ExecutionTime().Should().BeLessThanOrEqualTo(20000.Milliseconds());
            output.WriteLine($"First GetToken call took [{sw.ElapsedMilliseconds}] milliseconds");

            await Task.Delay(100);

            for (int i = 0; i < 5; i++)
            {
                sw = Stopwatch.StartNew();
                someAction.ExecutionTime().Should().BeLessThanOrEqualTo(750.Milliseconds());
                output.WriteLine($"Interation [{i}] for sequential GetToken call took [{sw.ElapsedMilliseconds}] milliseconds");
            }
        }

        [Fact]
        [Trait("feature", "beta")]
        public async void GetToken_Should_Be_Less_Than_750ms_With_CliCredential()
        {
            var credentials = CredentialsManager.GetAzureCliCredential();

            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

            Action someAction = () => credentials.GetToken(new TokenRequestContext(scopes), new CancellationToken());

            var sw = Stopwatch.StartNew();
            someAction.ExecutionTime().Should().BeLessThanOrEqualTo(600.Milliseconds());
            output.WriteLine($"GetToken call took [{sw.ElapsedMilliseconds}] milliseconds");
        }

        [Fact]
        [Trait("feature", "beta")]
        public void DefaultCredentials_Should_Be_Correct()
        {
            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };

            var token = _fixture.Credential.GetToken(new TokenRequestContext(scopes));

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Token) as JwtSecurityToken;

            jsonToken.Should().NotBeNull();
            jsonToken.Claims.Should().HaveCountGreaterThan(1);

            foreach (var item in jsonToken.Claims)
            {
                output.WriteLine(item.ToString());
            }

            output.WriteLine($"");

            using (new AssertionScope())
            {
                jsonToken?.Claims.First(c => c.Type == "iss").Value.Should().Contain("e6299caf-b2d5-4341-a225-01f58dfad513");
                jsonToken?.Claims.First(c => c.Type == "app_displayname").Value.Should().Be("sp-datamesh-dev");
                jsonToken?.Claims.First(c => c.Type == "tid").Value.Should().Be("e6299caf-b2d5-4341-a225-01f58dfad513");
            }
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Environment_Variable_Should_Be_Correct()
        {
            var test1 = Environment.GetEnvironmentVariable("TEST_XXX");
            test1?.Should().Be(@"C:\ProgramFiles\dotnet");
        }

        [Theory]
        [InlineData(CredentialType.Enviroment)]
        [InlineData(CredentialType.Cli)]
        [Trait("feature", "beta")]
        public async Task Credential_Types_Should_Work_In_Right_Environment(CredentialType type)
        {
            TokenCredential credential;

            switch (type)
            {
                case CredentialType.Enviroment:
                    credential = CredentialsManager.GetEnvironmentCredential();
                    break;
                case CredentialType.Cli:
                    credential = CredentialsManager.GetAzureCliCredential();
                    break;
                case CredentialType.Default:
                    credential = CredentialsManager.GetDefaultCredential();
                    break;
                default:
                    throw new Exception($"Argument [{type}] is not supported.");
            }

            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
            var ctoken = new CancellationToken();
            var token = credential.GetToken(new TokenRequestContext(scopes), ctoken);
        }

        public async Task Credential_Manager_Should_Use_Options_Settings()
        {

        }

    }
}
