using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Sensor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("id_v1")]
        public string IdV1 { get; set; }

        [JsonProperty("product_data")]
        public ProductData ProductData { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("services")]
        public List<Service> Services { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
