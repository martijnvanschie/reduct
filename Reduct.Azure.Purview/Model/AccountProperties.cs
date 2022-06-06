using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class AccountProperties
    {
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdByObjectId")]
        public string CreatedByObjectId { get; set; }

        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }
    }
}
