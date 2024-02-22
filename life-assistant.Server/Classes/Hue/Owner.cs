using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Owner
    {
        [JsonProperty("rid")]
        public string Rid { get; set; }

        [JsonProperty("rtype")]
        public string Rtype { get; set; }
    }
}
