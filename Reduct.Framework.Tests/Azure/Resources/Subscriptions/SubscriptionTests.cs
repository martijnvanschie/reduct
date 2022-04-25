using Xunit;
using FluentAssertions;
using System;
using Reduct.Azure.Resources.Subscriptions;
using System.Threading.Tasks;
using Reduct.Framework.Tests.Fixtures;
using Azure.Identity;
using Microsoft.Extensions.Logging;

namespace Reduct.Framework.Tests.Azure.Resources.Subscriptions
{
    public class SubscriptionTests : IClassFixture<DefaultCredentialsFixture>
    {
        DefaultCredentialsFixture _fixture;
        DefaultAzureCredential _credential;
        ILoggerFactory _loggerFactory;

        public SubscriptionTests(DefaultCredentialsFixture fixture)
        {
            this._fixture = fixture;
            this._credential = fixture.Credential;
            this._loggerFactory = fixture.LoggerFactoryInstance;
        }

        [Theory]
        [InlineData("8f1f0350-55c8-4d2b-ac20-3782bc460213", "d7b18f81-d130-4634-8aa9-3ed401ebe2a1")]
        [InlineData("8f1f0350-55c8-4d2b-ac20-3782bc460213", "a7501af4-34d1-4d6b-bb91-efc664768104")]
        [InlineData("e6299caf-b2d5-4341-a225-01f58dfad513", "f3222608-49ac-4b9d-993a-f8983fe80300")]
        [InlineData("e6299caf-b2d5-4341-a225-01f58dfad513", "1f66194d-2728-4a2f-a20c-2cadf09856a5")]
        [InlineData("e6299caf-b2d5-4341-a225-01f58dfad513", "f48e62dd-9c7f-4c67-b691-1b3e3df41940")]
        public async Task Valid_SubscriptionId_With_Tenant_Should_Not_Be_Null(string tenantId, string subscriptionId)
        {
            var credential = Reduct.Azure.CredentialsManager.GetAzureCliCredential(tenantId);
            var sub = await SubscriptionUtils.GetSubscriptionAsync(credential, subscriptionId, tenantId: tenantId);

            sub.Should()
               .NotBeNull();

            sub.Data
               .Should()
               .NotBeNull();

            sub.Id.Name
               .Should()
               .Contain(subscriptionId);
        }

        [Theory]
        [InlineData("8f1f0350-55c8-4d2b-ac20-3782bc460213", "d7b18f81-d130-4634-8aa9-3ed401ebe2a1")]
        [InlineData("8f1f0350-55c8-4d2b-ac20-3782bc460213", "a7501af4-34d1-4d6b-bb91-efc664768104")]
        public async Task Valid_SubscriptionId_Should_Not_Be_Null(string tenantId, string subscriptionId)
        {
            var sub = await SubscriptionUtils.GetSubscriptionAsync(_credential, subscriptionId);

            sub.Should()
               .NotBeNull();

            sub.Data
               .Should()
               .NotBeNull();

            sub.Id.Name
               .Should()
               .Contain(subscriptionId);
        }

        [Fact]
        public async Task Empty_SubscriptionId_With_Default_Should_Throw_ArgumentException()
        {
            Func<Task> sub = () => SubscriptionUtils.GetSubscriptionAsync(_credential, "", true);

            await sub.Should()
                .ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Empty_SubscriptionId_Should_Throw_ArgumentException()
        {
            Func<Task> sub = () => SubscriptionUtils.GetSubscriptionAsync(_credential, "");
            await sub.Should()
                .ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task SubscriptionId_With_Spaces_Should_Throw_ArgumentException()
        {
            Func<Task> sub = () => SubscriptionUtils.GetSubscriptionAsync(_credential, "a7501af4 - 34d1 - 4d6b - bb91 - efc664768104");
            await sub.Should()
                .ThrowAsync<ArgumentException>();
        }
    }
}
