using Newtonsoft.Json;

public class DailyForecast
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("epochDate")]
    public long EpochDate { get; set; }

    [JsonProperty("sun")]
    public Sun Sun { get; set; }

    [JsonProperty("moon")]
    public Moon Moon { get; set; }

    [JsonProperty("temperature")]
    public Temperature Temperature { get; set; }

    [JsonProperty("realFeelTemperature")]
    public Temperature RealFeelTemperature { get; set; }

    [JsonProperty("realFeelTemperatureShade")]
    public Temperature RealFeelTemperatureShade { get; set; }

    [JsonProperty("hoursOfSun")]
    public double HoursOfSun { get; set; }

    [JsonProperty("degreeDaySummary")]
    public DegreeDaySummary DegreeDaySummary { get; set; }

    [JsonProperty("airAndPollen")]
    public List<AirAndPollen> AirAndPollen { get; set; }

    [JsonProperty("day")]
    public WeatherDay Day { get; set; }

    [JsonProperty("night")]
    public WeatherDay Night { get; set; }
}