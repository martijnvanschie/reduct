using Xunit;
using FluentAssertions;
using System;
using Reduct.Azure.Resources.Subscriptions;
using System.Threading.Tasks;
using Reduct.Framework.Tests.Fixtures;
using Azure.Identity;

namespace Reduct.Framework.Tests.Azure.Resources.Subscriptions
{
    public class SubscriptionTests : IClassFixture<DefaultCredentialsFixture>
    {
        DefaultCredentialsFixture _fixture;
        DefaultAzureCredential _credential;

        public SubscriptionTests(DefaultCredentialsFixture fixture)
        {
            this._fixture = fixture;
            this._credential = fixture.Credential;
        }

        [Theory]
        [InlineData("d7b18f81-d130-4634-8aa9-3ed401ebe2a1")]
        [InlineData("a7501af4-34d1-4d6b-bb91-efc664768104")]
        public async Task Valid_SubscriptionId_Should_Not_Be_Null(string subscriptionId)
        {
            var sub = await SubscriptionUtils.GetSubscriptionAsync(_credential, subscriptionId);

            sub.Should()
                .NotBeNull();

            sub.Id.Name.Should()
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
