using System.Text.Json.Serialization;

namespace Reduct.Azure.Purview.Model
{
    public class Collection : CollectionBase
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("systemData")]
        public SystemData SystemData { get; set; }

        [JsonPropertyName("collectionProvisioningState")]
        public string CollectionProvisioningState { get; set; }

        [JsonPropertyName("parentCollection")]
        public CollectionRef ParentCollection { get; set; }

        public static Collection Create(string collectionName, string friendlyName, string referenceName, string collectionDescription = "")
        {
            var description = string.IsNullOrEmpty(collectionDescription) ? $"Collection {friendlyName} created on {DateTime.UtcNow}" : collectionDescription;

            Collection collection = new Collection()
            {
                Name = collectionName,
                FriendlyName = friendlyName,
                Description = description,
                ParentCollection = new CollectionRef()
                {
                    ReferenceName = referenceName,
                    Type = "CollectionReference"
                }
            };

            return collection;
        }

        /// <summary>
        /// Formats the name to a generic name.
        /// </summary>
        /// <param name="value">The value representing the name.</param>
        /// <param name="prefix">The optional preefix for the name.</param>
        /// <returns></returns>
        public static string EncodeStringForName(string value, string prefix = "")
        {
            return EncodeString(prefix) + EncodeString(value);
        }

        private static string EncodeString(string value)
        {
            return value.ToLower().Replace(' ', '-').Replace(".", null);
        }
    }
}
