using Newtonsoft.Json;

public class WindDirection
{
    [JsonProperty("degrees")]
    public int Degrees { get; set; }

    [JsonProperty("localized")]
    public string Localized { get; set; }

    [JsonProperty("english")]
    public string English { get; set; }
}