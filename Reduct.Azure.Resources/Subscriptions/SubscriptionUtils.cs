using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using System.Text.RegularExpressions;

namespace Reduct.Azure.Resources.Subscriptions
{
    public class SubscriptionUtils
    {
        public static async Task<Subscription> GetSubscriptionAsync(TokenCredential credentials, string? subscriptionId = null, bool returnDefault = false, string tenantId = "")
        {
            ArmClient armClient = ArmClientManager.GetClient(credentials, tenantId);

            if (subscriptionId is null && returnDefault)
                return await armClient.GetDefaultSubscriptionAsync();

            if (subscriptionId is null || !IsValidSubscriptionId(subscriptionId))
            {
                throw new ArgumentException($"Provided subscription ID [{subscriptionId}] is not valid.");
            }

            var test = armClient.GetSubscriptions();
            var subscription = test.Where(s => s.Id.Name.Contains(subscriptionId))?.FirstOrDefault();
            return subscription;
        }

        public static bool IsValidSubscriptionId(string subscriptionId)
        {
            var rege = new Regex("^[0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12}$");
            var match = rege.IsMatch(subscriptionId);
            return match;
        }
    }
}
