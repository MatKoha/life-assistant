using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Dimming
    {
        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("dimming")]
        public Dimming DimmingValue { get; set; }
        [JsonProperty("brightness")]
        public double Brightness { get; set; }
    }
}
