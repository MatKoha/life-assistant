using Newtonsoft.Json;
using System.Security.Principal;

namespace life_assistant.Server.Classes.Hue
{
    public class Light
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("id_v1")]
        public string IdV1 { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("product_data")]
        public ProductData ProductData { get; set; }

        [JsonProperty("identify")]
        public Identify Identify { get; set; }

        [JsonProperty("on")]
        public On On { get; set; }

        [JsonProperty("dimming")]
        public Dimming Dimming { get; set; }

        [JsonProperty("dimming_delta")]
        public DimmingDelta DimmingDelta { get; set; }

        [JsonProperty("color_temperature")]
        public ColorTemperature ColorTemperature { get; set; }

        [JsonProperty("color_temperature_delta")]
        public ColorTemperatureDelta ColorTemperatureDelta { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("dynamics")]
        public Dynamics Dynamics { get; set; }

        [JsonProperty("alert")]
        public Alert Alert { get; set; }

        [JsonProperty("signaling")]
        public Signaling Signaling { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("effects")]
        public Effect Effects { get; set; }

        [JsonProperty("powerup")]
        public Powerup Powerup { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class DimmingDelta
    {
    }

    public class ColorTemperatureDelta
    {
    }

    public class Identify
    {
    }
}
