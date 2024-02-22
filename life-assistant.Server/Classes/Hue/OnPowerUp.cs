using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class OnPowerUp
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }
        [JsonProperty("on")]
        public On On { get; set; }
    }
}