using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Gamut
    {
        [JsonProperty("red")]
        public XY Red { get; set; }

        [JsonProperty("green")]
        public XY Green { get; set; }

        [JsonProperty("blue")]
        public XY Blue { get; set; }

        [JsonProperty("gamut_type")]
        public string GamutType { get; set; }
    }
}
