using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Alert
    {
        [JsonProperty("action_values")]
        public List<string> ActionValues { get; set; }
    }
}
