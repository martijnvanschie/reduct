using Azure.Core;
using Azure.Identity;

namespace Reduct.Azure
{
    public class CredentialsManager
    {
        static DefaultAzureCredential? _defaultCredential;
        static EnvironmentCredential? _environmentCredential;
        static AzureCliCredential? _cliCredential;

        /// <summary>
        ///   Specifies whether the <see cref="Azure.Identity.EnvironmentCredential"/> will be used
        ///   from the <see cref="Azure.Identity.DefaultAzureCredential"/> authentication flow.
        ///   Default value is <c>true</c>.
        /// </summary>
        public static bool EnableEnvironmentCredentials { get; set; } = true;

        /// <summary>
        ///   Specifies whether the <see cref="Azure.Identity.VisualStudioCodeCredential"/> or <see cref="Azure.Identity.VisualStudioCredential"/>
        ///   will be used from the <see cref="Azure.Identity.DefaultAzureCredential"/> authentication flow.
        ///   Default value is <c>false</c>.
        /// </summary>
        /// <remarks>
        /// This settings combines both the Visual Studio and Visual Studio Code credentials.
        /// </remarks>
        public static bool EnableVisualStudioCredentials { get; set; } = false;

        /// <summary>
        ///   Specifies whether the <see cref="Azure.Identity.AzureCliCredential"/> or <see cref="Azure.Identity.AzurePowerShellCredential"/>
        ///   will be used from the <see cref="Azure.Identity.DefaultAzureCredential"/> authentication flow.
        ///   Default value is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// This settings combines both the Cli and PowerShell credentials.
        /// </remarks>
        public static bool EnableShellCredentials { get; set; } = true;

        /// <summary>
        ///   Specifies whether the <see cref="Azure.Identity.ManagedIdentityCredential"/>
        ///   will be used from the <see cref="Azure.Identity.DefaultAzureCredential"/> authentication flow.
        ///   Default value is <c>true</c>.
        /// </summary>
        /// <remarks>
        /// This settings combines both the Cli and PowerShell credentials.
        /// </remarks>
        public static bool EnableManagedIdentityCredential { get; set; } = true;

        /// <summary>
        /// Creates a instance of <see cref="Azure.Identity.DefaultAzureCredential"/> based on the global option settings.
        /// Credential is than cached and returned on sequential calls.
        /// </summary>
        /// <param name="useFromCache">If set to <c>true</c> always returns a new instance of <see cref="Azure.Identity.DefaultAzureCredential"/></param>
        /// <returns></returns>
        public static DefaultAzureCredential GetDefaultCredential(bool useFromCache = true)
        {
            if (!useFromCache)
            {
                return CreateDefaultAzureCredential();
            }

            if (_defaultCredential is null)
            {
                _defaultCredential = CreateDefaultAzureCredential();
            }

            return _defaultCredential;
        }

        internal static DefaultAzureCredential CreateDefaultAzureCredential()
        {
            DefaultAzureCredentialOptions options = new DefaultAzureCredentialOptions()
            {
                ExcludeVisualStudioCodeCredential = !EnableVisualStudioCredentials,
                ExcludeVisualStudioCredential = !EnableVisualStudioCredentials,
                ExcludeAzureCliCredential = !EnableShellCredentials,
                ExcludeAzurePowerShellCredential = !EnableShellCredentials,
                ExcludeEnvironmentCredential = !EnableEnvironmentCredentials,
                ExcludeManagedIdentityCredential = !EnableManagedIdentityCredential,
                //ExcludeInteractiveBrowserCredential = false,
                //ExcludeSharedTokenCacheCredential = false
            };

            var credential = new DefaultAzureCredential(options);

            return credential;
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

        /// <summary>
        /// Returns a new instance of <see cref="AzureCliCredential"/> for the specified tenant.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static TokenCredential GetAzureCliCredential(string tenantId)
        {
            var cliCredential = new AzureCliCredential(new AzureCliCredentialOptions() { TenantId = tenantId } );
            return cliCredential;
        }
    }
}