using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class CollectionBase
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("friendlyName")]
        public string FriendlyName { get; set; }
    }
}
