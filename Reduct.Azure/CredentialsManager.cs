using Azure.Core;
using Azure.Identity;

namespace Reduct.Azure
{
    public class CredentialsManager
    {
        public static TokenCredential GetCredentials()
        {
            return new DefaultAzureCredential();
        }
    }
}