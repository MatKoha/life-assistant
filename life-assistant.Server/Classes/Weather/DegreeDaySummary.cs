using Newtonsoft.Json;

public class DegreeDaySummary
{
    [JsonProperty("heating")]
    public WeatherMeasurement Heating { get; set; }

    [JsonProperty("cooling")]
    public WeatherMeasurement Cooling { get; set; }
}