using System;
using Newtonsoft.Json;

public class HourlyWeatherForecastResponse
{
    [JsonProperty("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonProperty("epochDateTime")]
    public long EpochDateTime { get; set; }

    [JsonProperty("weatherIcon")]
    public int WeatherIcon { get; set; }

    [JsonProperty("iconPhrase")]
    public string IconPhrase { get; set; }

    [JsonProperty("hasPrecipitation")]
    public bool HasPrecipitation { get; set; }

    [JsonProperty("precipitationType")]
    public string PrecipitationType { get; set; }

    [JsonProperty("precipitationIntensity")]
    public string PrecipitationIntensity { get; set; }

    [JsonProperty("isDaylight")]
    public bool IsDaylight { get; set; }

    [JsonProperty("temperature")]
    public WeatherMeasurement Temperature { get; set; }

    [JsonProperty("realFeelTemperature")]
    public WeatherMeasurement RealFeelTemperature { get; set; }

    [JsonProperty("realFeelTemperatureShade")]
    public WeatherMeasurement RealFeelTemperatureShade { get; set; }

    [JsonProperty("wetBulbTemperature")]
    public WeatherMeasurement WetBulbTemperature { get; set; }

    [JsonProperty("dewPoint")]
    public WeatherMeasurement DewPoint { get; set; }

    [JsonProperty("wind")]
    public Wind Wind { get; set; }

    [JsonProperty("windGust")]
    public Wind WindGust { get; set; }

    [JsonProperty("relativeHumidity")]
    public int RelativeHumidity { get; set; }

    [JsonProperty("indoorRelativeHumidity")]
    public int IndoorRelativeHumidity { get; set; }

    [JsonProperty("visibility")]
    public WeatherMeasurement Visibility { get; set; }

    [JsonProperty("ceiling")]
    public WeatherMeasurement Ceiling { get; set; }

    [JsonProperty("uvIndex")]
    public int UVIndex { get; set; }

    [JsonProperty("uvIndexText")]
    public string UVIndexText { get; set; }

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

    [JsonProperty("totalLiquid")]
    public WeatherMeasurement TotalLiquid { get; set; }

    [JsonProperty("rain")]
    public WeatherMeasurement Rain { get; set; }

    [JsonProperty("snow")]
    public WeatherMeasurement Snow { get; set; }

    [JsonProperty("ice")]
    public WeatherMeasurement Ice { get; set; }

    [JsonProperty("cloudCover")]
    public int CloudCover { get; set; }

    [JsonProperty("evapotranspiration")]
    public WeatherMeasurement Evapotranspiration { get; set; }

    [JsonProperty("solarIrradiance")]
    public WeatherMeasurement SolarIrradiance { get; set; }

    [JsonProperty("mobileLink")]
    public string MobileLink { get; set; }

    [JsonProperty("link")]
    public string Link { get; set; }
}
