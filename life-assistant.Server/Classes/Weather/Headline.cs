using Newtonsoft.Json;

public class Headline
{
    [JsonProperty("effectiveDate")]
    public DateTime EffectiveDate { get; set; }

    [JsonProperty("effectiveEpochDate")]
    public long EffectiveEpochDate { get; set; }

    [JsonProperty("severity")]
    public int Severity { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("endDate")]
    public DateTime? EndDate { get; set; }

    [JsonProperty("endEpochDate")]
    public long? EndEpochDate { get; set; }

    [JsonProperty("mobileLink")]
    public string MobileLink { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }
}