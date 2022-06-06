using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Reduct.Azure.Purview.Model
{
    public class PurviewAccount
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("identity")]
        public PurviewAccountIdentity Identity { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("properties")]
        public AccountProperties Properties { get; set; }

        [JsonPropertyName("systemData")]
        public SystemData SystemData { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
