using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class XY
    {
        [JsonProperty("x")]
        public double X { get; set; }

        [JsonProperty("y")]
        public double Y { get; set; }
    }
}
