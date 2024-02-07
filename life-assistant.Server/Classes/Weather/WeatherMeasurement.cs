using Newtonsoft.Json;

public class WeatherMeasurement
{
    [JsonProperty("value")]
    public double Value { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }

    [JsonProperty("unitType")]
    public int UnitType { get; set; }
}