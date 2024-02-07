using Newtonsoft.Json;

public class WeatherDay
{
    [JsonProperty("icon")]
    public int Icon { get; set; }

    [JsonProperty("iconPhrase")]
    public string IconPhrase { get; set; }

    [JsonProperty("hasPrecipitation")]
    public bool HasPrecipitation { get; set; }

    [JsonProperty("precipitationType")]
    public string PrecipitationType { get; set; }

    [JsonProperty("precipitationIntensity")]
    public string PrecipitationIntensity { get; set; }

    [JsonProperty("shortPhrase")]
    public string ShortPhrase { get; set; }

    [JsonProperty("longPhrase")]
    public string LongPhrase { get; set; }

    [JsonProperty("precipitationProbability")]
    public int PrecipitationProbability { get; set; }

    [JsonProperty("thunderstormProbability")]
    public int ThunderstormProbability { get; set; }

    [JsonProperty("rainProbability")]
    public int RainProbability { get; set; }

    [JsonProperty("snowProbability")]
    public int SnowProbability { get; set; }

    [JsonProperty("iceProbability")]
    public int IceProbability { get; set; }

    [JsonProperty("wind")]
    public Wind Wind { get; set; }

    [JsonProperty("windGust")]
    public Wind WindGust { get; set; }

    [JsonProperty("totalLiquid")]
    public WeatherMeasurement TotalLiquid { get; set; }

    [JsonProperty("rain")]
    public WeatherMeasurement Rain { get; set; }

    [JsonProperty("snow")]
    public WeatherMeasurement Snow { get; set; }

    [JsonProperty("ice")]
    public WeatherMeasurement Ice { get; set; }

    [JsonProperty("hoursOfPrecipitation")]
    public double HoursOfPrecipitation { get; set; }

    [JsonProperty("hoursOfRain")]
    public double HoursOfRain { get; set; }

    [JsonProperty("hoursOfSnow")]
    public double HoursOfSnow { get; set; }

    [JsonProperty("hoursOfIce")]
    public double HoursOfIce { get; set; }

    [JsonProperty("cloudCover")]
    public int CloudCover { get; set; }
}