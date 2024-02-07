using Newtonsoft.Json;

public class Moon : Sun
{
    [JsonProperty("phase")]
    public string Phase { get; set; }

    [JsonProperty("age")]
    public int Age { get; set; }
}