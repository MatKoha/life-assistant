using Newtonsoft.Json;

public class Temperature
{
    [JsonProperty("minimum")]
    public WeatherMeasurement Minimum { get; set; }

    [JsonProperty("maximum")]
    public WeatherMeasurement Maximum { get; set; }
}