using Microsoft.Extensions.Logging;
using Reduct.Framework.Tests.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using Reduct.Azure.Purview.Account;
using FluentAssertions;
using Reduct.Azure.Purview.Model;

namespace Reduct.Framework.Tests.Azure.Purview.Account
{
    public class CollectionsClientUnitTests : IClassFixture<DefaultCredentialsFixture>
    {
        static DefaultCredentialsFixture _fixture;
        static ITestOutputHelper _output;
        static ILoggerFactory _loggerFactory;

        static CollectionsClient _collectionClient;
        static string _accountName = "pur-mvs-test";

        public CollectionsClientUnitTests(DefaultCredentialsFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _loggerFactory = fixture.LoggerFactoryInstance;
            _output = output;
        }

        private static CollectionsClient GetCollectionsClient()
        {
            if (_collectionClient is null)
            {
                _collectionClient = new CollectionsClient(_accountName, _fixture.Credential, _loggerFactory.CreateLogger<CollectionsClient>());
            }

            return _collectionClient;
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Should_Create_A_CollectionClient()
        {
            Action act = () => GetCollectionsClient();
            act.Should().NotThrow();
        }

        [Fact]
        public async Task Should_Throw_ArgumentException_For_Missing_AccountName()
        {
            Action act = () => new CollectionsClient("", _fixture.Credential, _loggerFactory.CreateLogger<CollectionsClient>());

            act.Should().Throw<ArgumentException>()
                .WithMessage("*No accountname was passed*");
        }

        [Fact]
        public async Task Should_Throw_ArgumentException_For_Missing_Credentials()
        {
            Action act = () => new CollectionsClient("somename", null, _loggerFactory.CreateLogger<CollectionsClient>());

            act.Should().Throw<ArgumentNullException>()
                .WithMessage("*No credentials where passed*");
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Should_Get_All_Collections()
        {
            var client = GetCollectionsClient();
            await client.GetAllCollectionsAsync();
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Should_Get_A_Single_Collections()
        {
            var collectionName = _accountName;

            var client = GetCollectionsClient();
            var collection = await client.GetCollectionAsync(collectionName);

            collection.Name.Should().Be(_accountName);
            collection.FriendlyName.Should().Be(collectionName);
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Should_Get_All_Collections_Children()
        {
            var client = GetCollectionsClient();
            await client.GetCollectionsChildrenAsync("pur-mvs-test");
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task ShouldThrowCollectionNotFoundExceptionForNoneExistingCollection()
        {
            var client = GetCollectionsClient();
            Func<Task> act = () => client.GetCollectionAsync("doesnotexist");
            await act.Should().ThrowAsync<CollectionNotFoundException>();
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task ShouldThrowExceptionOnNoneExistingCollections()
        {
            var client = GetCollectionsClient();
            Func<Task> act = () => client.DeleteCollectionAsync("doesnotexist");
            await act.Should().ThrowAsync<CollectionNotFoundException>();
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task Should_Return_Unknown_Host_Error()
        {
            var wrongAccountName = "this-should-not-be-found";
            var client = new CollectionsClient(wrongAccountName, _fixture.Credential, _loggerFactory.CreateLogger<CollectionsClient>());

            Func<Task> act = () => client.GetAllCollectionsAsync();
            await act.Should().ThrowAsync<AccountDataPlaneCommunicationException>()
                .WithMessage("*No such host is known*");
        }
    }

    public class CollectionsClientFunctionalTests : IClassFixture<DefaultCredentialsFixture>
    {
        static DefaultCredentialsFixture _fixture;
        static ITestOutputHelper _output;
        static ILoggerFactory _loggerFactory;

        static CollectionsClient _collectionClient;
        static string _accountName = "pur-mvs-test";

        public CollectionsClientFunctionalTests(DefaultCredentialsFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _loggerFactory = fixture.LoggerFactoryInstance;
            _output = output;
        }

        private static CollectionsClient GetCollectionsClient()
        {
            if (_collectionClient is null)
            {
                _collectionClient = new CollectionsClient(_accountName, _fixture.Credential, _loggerFactory.CreateLogger<CollectionsClient>());
            }

            return _collectionClient;
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task ShouldCreateAndDeleteACollection()
        {
            var collectionName = Guid.NewGuid().ToString().Substring(0, 6);
            var collection = Collection.Create(collectionName, $"XUnit Collection {collectionName}", "pur-mvs-test");

            var client = GetCollectionsClient();
            var collection2 = await client.CreateOrUpdateCollection(collection);

            await client.DeleteCollectionAsync(collectionName);
        }

        [Fact]
        [Trait("feature", "beta")]
        public async Task ShouldThrowExceptionWhenDeletingReferencedCollection()
        {
            var levelOneCollectionName = "col-level-one";
            var levelTwoCollectionName = "col-level-two";

            var levelOneCollection = Collection.Create(levelOneCollectionName, $"XUnit Collection {levelOneCollectionName}", "pur-mvs-test");
            var levelTwoCollection = Collection.Create(levelTwoCollectionName, $"XUnit Collection {levelTwoCollectionName}", levelOneCollectionName);

            var client = GetCollectionsClient();
            var collection1 = await client.CreateOrUpdateCollection(levelOneCollection);
            var collection2 = await client.CreateOrUpdateCollection(levelTwoCollection);

            Func<Task> act = () => client.DeleteCollectionAsync(levelOneCollectionName);
            await act.Should().ThrowAsync<global::Azure.RequestFailedException>();

            await client.DeleteCollectionAsync(levelTwoCollectionName);
            await client.DeleteCollectionAsync(levelOneCollectionName);

            var t = client.GetCollectionAsync(levelOneCollectionName);




        }
    }
}
