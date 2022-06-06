using Azure.Analytics.Purview.Account;
using Azure.Core;
using Microsoft.Extensions.Logging;
using Reduct.Azure.Purview.Model;
using Reduct.Core.Logging;

namespace Reduct.Azure.Purview.Account
{
    /// <summary>
    /// This class wraps the functionality provided by the Account Data Plane Accounts API
    /// https://docs.microsoft.com/en-us/rest/api/purview/accountdataplane/accounts
    /// </summary>
    public class AccountsClient : AccountDataPlaneBase
    {
        private readonly ILogger<AccountsClient> _logger;

        public AccountsClient(string accountName, TokenCredential credential, ILogger<AccountsClient>? logger = null) : base(accountName, credential)
        {
            _logger = logger ?? LoggingManager.GetNullLogger<AccountsClient>();
        }

        public async Task<PurviewAccount> GetAccountInfo()
        {
            var response = await _client.GetAccountPropertiesAsync();
            var account = response.Content.ToObjectFromJson<PurviewAccount>();
            return account;
        }
    }
}
