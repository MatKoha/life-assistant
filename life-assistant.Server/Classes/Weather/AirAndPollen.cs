using Newtonsoft.Json;

public class AirAndPollen
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public int Value { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("categoryValue")]
    public int CategoryValue { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}