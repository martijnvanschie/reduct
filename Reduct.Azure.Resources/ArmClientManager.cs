using Azure.Core;
using Azure.ResourceManager;

namespace Reduct.Azure.Resources
{
    public class ArmClientManager
    {
        private static ArmClient _armClient;

        internal static ArmClient GetClient(TokenCredential credential)
        {
            if (_armClient == null)
            {
                _armClient = new ArmClient(credential);
            }

            return _armClient;
        }

    }
}