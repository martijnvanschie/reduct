using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class CollectionRef
    {
        [JsonPropertyName("lastModifiedAt")]
        public DateTime? LastModifiedAt { get; set; }

        [JsonPropertyName("referenceName")]
        public string ReferenceName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
