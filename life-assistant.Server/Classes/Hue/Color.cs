using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Color
    {
        [JsonProperty("xy")]
        public XY Xy { get; set; }

        [JsonProperty("gamut")]
        public Gamut Gamut { get; set; }

        [JsonProperty("gamut_type")]
        public string GamutType { get; set; }
    }
}
