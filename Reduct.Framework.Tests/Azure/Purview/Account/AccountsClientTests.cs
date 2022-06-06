using Microsoft.Extensions.Logging;
using Reduct.Framework.Tests.Fixtures;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Reduct.Azure.Purview.Account;
using FluentAssertions;

namespace Reduct.Framework.Tests.Azure.Purview.Account
{
    public class AccountsClientTests : IClassFixture<DefaultCredentialsFixture>
    {
        DefaultCredentialsFixture _fixture;
        ITestOutputHelper _output;
        ILoggerFactory _loggerFactory;

        public AccountsClientTests(DefaultCredentialsFixture fixture, ITestOutputHelper output)
        {
            this._fixture = fixture;
            this._loggerFactory = fixture.LoggerFactoryInstance;
            this._output = output;
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Should_Get_Account()
        {
            var accountName = "pur-mvs-test";
            var client = new AccountsClient(accountName, _fixture.Credential, _loggerFactory.CreateLogger<AccountsClient>());
            var account = await client.GetAccountInfo();

            account.Name.Should().Be(accountName);
        }
    }
}
