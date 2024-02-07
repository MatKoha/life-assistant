using Newtonsoft.Json;

public class Wind
{
    [JsonProperty("speed")]
    public WeatherMeasurement Speed { get; set; }

    [JsonProperty("direction")]
    public WindDirection Direction { get; set; }
}