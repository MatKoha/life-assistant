using Newtonsoft.Json;

public class ApiError
{
    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }
}