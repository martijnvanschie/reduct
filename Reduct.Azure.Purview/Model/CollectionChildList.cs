using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class CollectionChildList
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("value")]
        public List<CollectionBase> Collections { get; set; }
    }
}
