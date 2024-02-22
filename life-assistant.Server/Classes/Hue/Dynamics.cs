using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Dynamics
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("status_values")]
        public List<string> StatusValues { get; set; }

        [JsonProperty("speed")]
        public int Speed { get; set; }

        [JsonProperty("speed_valid")]
        public bool SpeedValid { get; set; }
    }
}
