using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class PurviewAccountIdentity
    {
        [JsonPropertyName("principalId")]
        public string PrincipalId { get; set; }

        [JsonPropertyName("tenantId")]
        public string TenantId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
