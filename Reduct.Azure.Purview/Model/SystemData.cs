using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class SystemData
    {
        [JsonPropertyName("createdBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("createdByType")]
        public string CreatedByType { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("lastModifiedBy")]
        public string LastModifiedBy { get; set; }

        [JsonPropertyName("lastModifiedByType")]
        public string LastModifiedByType { get; set; }

        [JsonPropertyName("lastModifiedAt")]
        public DateTime LastModifiedAt { get; set; }
    }
}
