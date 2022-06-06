using Azure.Analytics.Purview.Account;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reduct.Azure.Purview.Account
{
    public class AccountDataPlaneBase
    {
        private const string EXCEPTION_NO_ACCOUNTNAME = "No accountname was passed to the constructor.";
        private const string EXCEPTION_CREDENTIALS_NULL = "No credentials where passed to the constructor.";

        internal Uri _uri;
        internal PurviewAccountClient _client;

        public AccountDataPlaneBase(string accountName, TokenCredential credential)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                throw new ArgumentException(EXCEPTION_NO_ACCOUNTNAME, nameof(accountName));
            }

            if (credential == null)
            {
                throw new ArgumentNullException(EXCEPTION_CREDENTIALS_NULL, nameof(credential));
            }

            _uri = new Uri($"https://{accountName}.purview.azure.com/account");
            _client = new PurviewAccountClient(_uri, credential);
        }
    }
}
