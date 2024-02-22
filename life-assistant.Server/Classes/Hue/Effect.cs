using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class Effect
    {
        [JsonProperty("status_values")]
        public List<string> StatusValues { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("effect_values")]
        public List<string> EffectValues { get; set; }
    }
}
