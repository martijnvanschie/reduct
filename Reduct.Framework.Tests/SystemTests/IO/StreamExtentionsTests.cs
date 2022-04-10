using Reduct.Framework.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Reduct.System.IO;
using FluentAssertions;

namespace Reduct.Framework.Tests.SystemTests.IO
{
    public class StreamExtentionsTests : IClassFixture<DefaultFixture>
    {
        ITestOutputHelper _output;
        DefaultFixture _fixture;

        public StreamExtentionsTests(DefaultFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            _output = output ?? throw new ArgumentNullException(nameof(output));

            _output.WriteLine($"{nameof(StreamExtentionsTests)} initialized.");
        }

        [Fact]
        [Trait("feature", "beta")]
        public void ReadInt_Should_Return_Integer()
        {
            var expectedValue = 1000;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write(expectedValue);
                }

                stream.Position = 0;
                var intValue = stream.ReadInt32(4);
                intValue.Should().Be(expectedValue);
            }
        }

        [Fact]
        [Trait("feature", "beta")]
        public void ReadString_Should_Return_Integer()
        {
            var expectedValue = "1000";

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write(expectedValue);
                }

                stream.Position = 0;
                var intValue = stream.ReadString(5);
                intValue.Should().Be(expectedValue);
            }
        }

        [Fact]
        [Trait("feature", "beta")]
        public void ReadInt_Should_Return_Error_When_Not_Int()
        {
            var expectedValue = "1000";

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
                {
                    writer.Write(expectedValue);
                }

                stream.Position = 0;
                var intValue = stream.ReadInt32(4);
            }
        }
    }
}
