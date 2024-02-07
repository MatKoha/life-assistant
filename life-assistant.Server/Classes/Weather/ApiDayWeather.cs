using System;
using System.Globalization;
using Newtonsoft.Json;

public class ApiDayWeather
{
    [JsonProperty("date")]
    public DateTime Date { get; set; }
    [JsonProperty("sunRise")]
    public DateTime SunRise { get; set; }
    [JsonProperty("sunSet")]
    public DateTime SunSet { get; set; }
    [JsonProperty("moonRise")]
    public DateTime MoonRise { get; set; }
    [JsonProperty("moonSet")]
    public DateTime MoonSet { get; set; }
    [JsonProperty("moonPhase")]
    public string MoonPhase { get; set; }
    [JsonProperty("moonAge")]
    public int MoonAge { get; set; }
    [JsonProperty("minTemp")]
    public double MinTemp { get; set; }
    [JsonProperty("maxTemp")]
    public double MaxTemp { get; set; }
    [JsonProperty("realFeelMinTemp")]
    public double RealFeelMinTemp { get; set; }
    [JsonProperty("realFeelMaxTemp")]
    public double RealFeelMaxTemp { get; set; }
    [JsonProperty("type")]
    public int Type { get; set; }
    [JsonProperty("typeString")]
    public string TypeString { get; set; }
    [JsonProperty("windSpeed")]
    public int WindSpeed { get; set; }
    [JsonProperty("totalLiquid")]
    public double TotalLiquid { get; set; }
    [JsonProperty("precipitationProbability")]
    public int PrecipitationProbability { get; set; }


    public ApiDayWeather(DailyForecast day)
    {
        this.Date = day.Date;
        this.SunRise = day.Sun.Rise;
        this.SunSet = day.Sun.Set;
        this.MoonRise = day.Moon.Rise;
        this.MoonSet = day.Moon.Set;
        this.MoonAge = day.Moon.Age;
        this.MoonPhase = day.Moon.Phase;
        this.MinTemp = day.Temperature.Minimum.Value;
        this.MaxTemp = day.Temperature.Maximum.Value;
        this.RealFeelMinTemp = day.RealFeelTemperature.Minimum.Value;
        this.RealFeelMaxTemp = day.RealFeelTemperature.Maximum.Value;
        this.Type = day.Day.Icon;
        this.TypeString = day.Day.IconPhrase;
        this.WindSpeed = (int)Math.Round(day.Day.Wind.Speed.Value * (1000.0 / 3600.0));
        this.TotalLiquid = day.Day.TotalLiquid.Value;
        this.PrecipitationProbability = day.Day.PrecipitationProbability;   
    }
}



