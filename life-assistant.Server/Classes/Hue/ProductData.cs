using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class ProductData
    {
        [JsonProperty("model_id")]
        public string ModelId { get; set; }

        [JsonProperty("manufacturer_name")]
        public string ManufacturerName { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("product_archetype")]
        public string ProductArchetype { get; set; }

        [JsonProperty("certified")]
        public bool Certified { get; set; }

        [JsonProperty("software_version")]
        public string SoftwareVersion { get; set; }

        [JsonProperty("function")]
        public string Function { get; set; }
    }
}
