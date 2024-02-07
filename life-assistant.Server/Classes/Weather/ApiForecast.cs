using System;
using Newtonsoft.Json;

public class ApiForecast
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }
    [JsonProperty("type")]
    public int Type { get; set; }
    [JsonProperty("typeString")]
    public string TypeString { get; set; }
    [JsonProperty("temperature")]
    public double Temperature { get; set; }
    [JsonProperty("realFeelTemperature")]
    public double RealFeelTemperature { get; set; }
    [JsonProperty("isDayLight")]
    public bool IsDayLight { get; set; }
    [JsonProperty("windSpeed")]
    public int WindSpeed { get; set; }
    [JsonProperty("totalLiquid")]
    public double TotalLiquid { get; set; }
    [JsonProperty("precipitationProbability")]
    public int PrecipitationProbability { get; set; }

    public ApiForecast(HourlyWeatherForecastResponse forecast)
    {
        this.Date = forecast.DateTime;
        this.Type = forecast.WeatherIcon;
        this.TypeString = forecast.IconPhrase;
        this.Temperature = forecast.Temperature.Value;
        this.RealFeelTemperature = forecast.RealFeelTemperature.Value;
        this.IsDayLight = forecast.IsDaylight;
        this.WindSpeed = (int)Math.Round(forecast.Wind.Speed.Value * (1000.0 / 3600.0));
        this.TotalLiquid = forecast.TotalLiquid.Value;
        this.PrecipitationProbability = forecast.PrecipitationProbability;
    }
}
