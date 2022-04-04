using Azure.Core;
using Azure.Identity;

namespace Reduct.Azure
{
    public class CredentialsManager
    {
        static DefaultAzureCredential? _defaultCredential;
        static EnvironmentCredential? _environmentCredential;
        static AzureCliCredential? _cliCredential;

        public static DefaultAzureCredential GetDefaultCredential()
        {
            if (_defaultCredential is null)
            {

                DefaultAzureCredentialOptions options = new DefaultAzureCredentialOptions()
                {
                    ExcludeVisualStudioCodeCredential = false,
                    ExcludeVisualStudioCredential = false
                };

                _defaultCredential = new DefaultAzureCredential(options);
            }

            return _defaultCredential;
        }

        [Obsolete("This methods signature changed to DefaultAzureCredentials() and will be removed in future releases. " +
            "A new helper method was introduced wich takes a type as input. GetCredential(CredentialType type)", false)]
        public static DefaultAzureCredential GetCredentials()
        {
            if (_defaultCredential is null)
            {

                DefaultAzureCredentialOptions options = new DefaultAzureCredentialOptions()
                {
                    ExcludeVisualStudioCodeCredential = false,
                    ExcludeVisualStudioCredential = false
                };

                _defaultCredential = new DefaultAzureCredential(options);
            }

            return _defaultCredential;
        }

        public static TokenCredential GetCredential(CredentialType type)
        {
            switch (type)
            {
                case CredentialType.Default:
                    return GetDefaultCredential();
                case CredentialType.Cli:
                    return GetAzureCliCredential();
                case CredentialType.Enviroment:
                    return GetEnvironmentCredential();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }
        }

        public static TokenCredential GetEnvironmentCredential()
        {
            if (_environmentCredential is null)
            {
                _environmentCredential = new EnvironmentCredential();
            }

            return _environmentCredential;
        }

        public static TokenCredential GetAzureCliCredential()
        {
            if (_cliCredential is null)
            {
                _cliCredential = new AzureCliCredential();
            }

            return _cliCredential;
        }
    }
}