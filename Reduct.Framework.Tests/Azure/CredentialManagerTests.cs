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

namespace Reduct.Framework.Tests.Azure
{

    public class CredentialManagerTests : IClassFixture<DefaultCredentialsFixture>
    {
        DefaultCredentialsFixture _fixture;

        public CredentialManagerTests(DefaultCredentialsFixture fixture)
        {
            this._fixture = fixture;
        }

        [Fact]
        [Trait("category", "trial")]
        public void DefaultCredentials_Should_Be_Correct()
        {
            string[] scopes = new string[] { "https://graph.microsoft.com/.default" };
            var token = _fixture.Credential.GetToken(new TokenRequestContext(scopes));

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.Token) as JwtSecurityToken;

            jsonToken.Should().NotBeNull();

            jsonToken?.Claims.First(c => c.Type == "app_displayname").Value.Should().Be("sp-datamesh-dev");
            jsonToken?.Claims.First(c => c.Type == "tid").Value.Should().Be("e6299caf-b2d5-4341-a225-01f58dfad513");
        }

        [Fact]
        [Trait("category", "trial")]
        public async Task Environment_Variable_Should_Be_Correct()
        {
            var test1 = Environment.GetEnvironmentVariable("TEST_XXX");
            test1?.Should().Be(@"C:\ProgramFiles\dotnet");
        }

        [Theory]
        [InlineData(CredentialType.Enviroment)]
        [InlineData(CredentialType.Cli)]
        [Trait("category", "trial")]
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
    }
}
