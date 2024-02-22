using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class ColorTemperature
    {
        [JsonProperty("mirek")]
        public int? Mirek { get; set; }

        [JsonProperty("mirek_valid")]
        public bool MirekValid { get; set; }

        [JsonProperty("mirek_schema")]
        public MirekSchema MirekSchema { get; set; }
    }
}
