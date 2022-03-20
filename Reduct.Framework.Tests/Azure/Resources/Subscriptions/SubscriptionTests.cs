using Xunit;
using FluentAssertions;
using Reduct.System;
using System;
using Xunit.Abstractions;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Reduct.Azure;
using Reduct.Azure.Resources.Subscriptions;
using System.Threading.Tasks;

namespace Reduct.Framework.Tests.Azure.Resources.Subscriptions
{
    public class SubscriptionTests
    {
        [Theory]
        [InlineData("d7b18f81-d130-4634-8aa9-3ed401ebe2a1")]
        [InlineData("a7501af4-34d1-4d6b-bb91-efc664768104")]
        public async Task Valid_SubscriptionId_Should_Not_Be_Null(string subscriptionId)
        {
            var credentials = CredentialsManager.GetCredentials();

            var sub = await SubscriptionUtils.GetSubscriptionAsync(credentials, subscriptionId);

            sub.Should()
                .NotBeNull();

            sub.Id.Name.Should()
                .Contain(subscriptionId);

        }

        [Fact]
        public async Task Empty_SubscriptionId_With_Default_Should_Throw_ArgumentException()
        {
            var credentials = CredentialsManager.GetCredentials();

            Func<Task> sub = () => SubscriptionUtils.GetSubscriptionAsync(credentials, "", true);

            await sub.Should()
                .ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Empty_SubscriptionId_Should_Throw_ArgumentException()
        {
            var credentials = CredentialsManager.GetCredentials();

            Func<Task> sub = () => SubscriptionUtils.GetSubscriptionAsync(credentials, "");
            await sub.Should()
                .ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task SubscriptionId_With_Spaces_Should_Throw_ArgumentException()
        {
            var credentials = CredentialsManager.GetCredentials();

            Func<Task> sub = () => SubscriptionUtils.GetSubscriptionAsync(credentials, "a7501af4 - 34d1 - 4d6b - bb91 - efc664768104");
            await sub.Should()
                .ThrowAsync<ArgumentException>();
        }
    }
}
