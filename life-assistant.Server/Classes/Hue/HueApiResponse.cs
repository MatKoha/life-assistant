using Newtonsoft.Json;

namespace life_assistant.Server.Classes.Hue
{
    public class HueApiResponse<T>
    {
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }

        [JsonProperty("data")]
        public List<T> Data { get; set; }
    }
}
