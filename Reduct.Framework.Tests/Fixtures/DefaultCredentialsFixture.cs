using Azure.Identity;
using Reduct.Azure;
using System;

namespace Reduct.Framework.Tests.Fixtures
{
    public class DefaultCredentialsFixture : IDisposable
    {
        internal DefaultAzureCredential Credential;

        public DefaultCredentialsFixture()
        {
            Credential = CredentialsManager.GetCredentials();
        }

        public void Dispose()
        {
            // Nothing to do
        }
    }
}
