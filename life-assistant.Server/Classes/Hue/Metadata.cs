using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Metadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("archetype")]
        public string Archetype { get; set; }

        [JsonProperty("function")]
        public string Function { get; set; }
    }
}
