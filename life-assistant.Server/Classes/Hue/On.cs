using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class On
    {
        [JsonProperty("on")]
        public bool IsOn { get; set; }
    }
}
