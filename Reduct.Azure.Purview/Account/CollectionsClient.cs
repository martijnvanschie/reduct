using Azure.Analytics.Purview.Account;
using Azure.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reduct.Core.Logging;
using Reduct.Azure.Purview.Model;

namespace Reduct.Azure.Purview.Account
{
    /// <summary>
    /// This class wraps the functionality provided by the Account Data Plane Collections API
    /// https://docs.microsoft.com/en-us/rest/api/purview/accountdataplane/collections
    /// </summary>
    public class CollectionsClient : AccountDataPlaneBase
    {
        private readonly ILogger<CollectionsClient> _logger;

        public CollectionsClient(string accountName, TokenCredential credential, ILogger<CollectionsClient>? logger = null) : base(accountName, credential)
        {
            _logger = logger ?? LoggingManager.GetNullLogger<CollectionsClient>();
        }

        public async Task<Collection> GetCollectionAsync(string collectionName)
        {
            try
            {
                var colClient = _client.GetCollectionClient(collectionName);
                var response2 = await colClient.GetCollectionAsync();
                var collectionsResponse = response2.Content.ToObjectFromJson<Collection>();
                return collectionsResponse;
            }
            catch (global::Azure.RequestFailedException ex)
            {
                if (ex.Status == 404)
                {
                    throw new CollectionNotFoundException($"Purview returned status code 404. This indicates the collection with name [{collectionName}] could not be found.", ex);
                }

                throw;
            }
        }

        public async Task<CollectionList> GetAllCollectionsAsync()
        {
            try
            {
                var response2 = await _client.GetCollectionsAsync();
                var collectionsResponse = response2.Content.ToObjectFromJson<CollectionList>();
                return collectionsResponse;
            }
            catch (global::Azure.RequestFailedException ex)
            {
                _logger.LogWarning($"Error while sending request to Purview API. [{ex.Message}]");
                throw new AccountDataPlaneCommunicationException(ex.Message, ex);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is not null)
                {
                    if (ex.InnerException is global::Azure.RequestFailedException)
                    {
                        throw new AccountDataPlaneCommunicationException(ex.InnerException.Message, ex.InnerException);
                    }
                }

                throw new AccountDataPlaneException(ex.Message, ex);
            }
        }

        public async Task<CollectionChildList> GetCollectionsChildrenAsync(string collectionName)
        {
            try
            {
                var colClient = _client.GetCollectionClient(collectionName);
                var response2 = await colClient.ListChildCollectionNamesAsync();
                var collectionsResponse = response2.Content.ToObjectFromJson<CollectionChildList>();
                return collectionsResponse;
            }
            catch (global::Azure.RequestFailedException ex)
            {
                _logger.LogWarning($"Error while sending request to Purview API. [{ex.Message}]");
                throw new AccountDataPlaneCommunicationException(ex.Message, ex);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is not null)
                {
                    if (ex.InnerException is global::Azure.RequestFailedException)
                    {
                        throw new AccountDataPlaneCommunicationException(ex.InnerException.Message, ex.InnerException);
                    }
                }

                throw new AccountDataPlaneException(ex.Message, ex);
            }
        }

        public async Task DeleteCollectionAsync(string collectionName)
        {
            try
            {
                var colClient = _client.GetCollectionClient(collectionName);
                var response2 = await colClient.DeleteCollectionAsync();

                if (response2.Status != 204)
                {
                    throw new AccountDataPlaneException($"Failed to delete collection [{collectionName}]. Reason: {response2.Content.ToString()}");
                }
            }
            catch (global::Azure.RequestFailedException ex)
            {
                if (ex.Status == 404)
                {
                    throw new CollectionNotFoundException($"Purview returned status code 404. This indicates the collection with name [{collectionName}] could not be found.", ex);
                }

                throw;
            }
        }

        public async Task<Collection> CreateOrUpdateCollection(Collection collection)
        {
            var colClient = _client.GetCollectionClient(collection.Name);
            var content = RequestContent.Create(collection);
            var response2 = await colClient.CreateOrUpdateCollectionAsync(content);
            return response2.Content.ToObjectFromJson<Collection>();
        }
    }
}
