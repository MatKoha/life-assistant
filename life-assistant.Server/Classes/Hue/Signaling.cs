using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Signaling
    {
        [JsonProperty("signal_values")]
        public List<string> SignalValues { get; set; }
    }
}
