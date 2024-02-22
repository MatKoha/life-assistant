using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class MirekSchema
    {
        [JsonProperty("mirek_minimum")]
        public int MirekMinimum { get; set; }

        [JsonProperty("mirek_maximum")]
        public int MirekMaximum { get; set; }
    }
}
