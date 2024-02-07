using Newtonsoft.Json;

public class DailyWeatherForecastResponse
{
    [JsonProperty("headLine")]
    public Headline Headline { get; set; }

    [JsonProperty("dailyForecasts")]
    public List<DailyForecast> DailyForecasts { get; set; }
}