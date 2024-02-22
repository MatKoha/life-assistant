using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Powerup
    {
        [JsonProperty("preset")]
        public string Preset { get; set; }

        [JsonProperty("configured")]
        public bool Configured { get; set; }

        [JsonProperty("on")]
        public OnPowerUp On { get; set; }

        [JsonProperty("dimming")]
        public Dimming Dimming { get; set; }

        [JsonProperty("color")]
        public Color Color { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
