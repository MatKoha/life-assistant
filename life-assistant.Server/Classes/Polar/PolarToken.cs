using Newtonsoft.Json;

namespace server.Classes
{
    public class PolarToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int Expires { get; set; }

        [JsonProperty("x_user_id")]
        public int MemberId { get; set; }
    }
}
