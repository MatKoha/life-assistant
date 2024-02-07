using Newtonsoft.Json;

public class Sun
{
    [JsonProperty("rise")]
    public DateTime Rise { get; set; }

    [JsonProperty("epochRise")]
    public long EpochRise { get; set; }

    [JsonProperty("set")]
    public DateTime Set { get; set; }

    [JsonProperty("epochSet")]
    public long EpochSet { get; set; }
}