using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class ApiHueHomeState
    {
        [JsonProperty("lightState")]
        public bool LightState { get; set; }
    }
}
