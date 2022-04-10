using Azure.Identity;
using Reduct.Azure;
using Xunit.Abstractions;

namespace Reduct.Framework.Tests.Fixtures
{
    public class DefaultCredentialsFixture : DefaultFixture
    {
        internal DefaultAzureCredential Credential;

        public DefaultCredentialsFixture()
        {
            Credential = CredentialsManager.GetDefaultCredential();
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
