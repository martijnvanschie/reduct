using Azure.Core;
using Azure.ResourceManager;

namespace Reduct.Azure.Resources
{
    public class ArmClientManager
    {
        private static IDictionary<string, ArmClient> _clients = new Dictionary<string, ArmClient>();

        private static ArmClient _armClient;

        internal static ArmClient GetClient(TokenCredential credential, string tenantId = "")
        {
            if (_clients.ContainsKey(tenantId))
            {
                return _clients[tenantId];
            }

            var armClient = new ArmClient(credential);
            _clients.Add(tenantId, armClient);
            return armClient;



            //if (_armClient == null)
            //{
            //    _armClient = new ArmClient(credential);
            //}

            //return _armClient;
        }

    }
}