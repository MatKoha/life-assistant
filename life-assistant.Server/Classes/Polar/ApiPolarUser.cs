using Newtonsoft.Json;

namespace server.Classes
{
    public class ApiPolarUser
    {
        [JsonProperty("polar-user-id")]
        public int PolarUserId { get; set; }
        [JsonProperty("first-name")]
        public string FirstName { get; set; }
        [JsonProperty("last-name")]
        public string LastName { get; set; }
        [JsonProperty("registration-date")]
        public DateTime RegistrationDate { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("weight")]
        public double Weight { get; set; }
        [JsonProperty("height")]
        public double Height { get; set; }
    }
}
