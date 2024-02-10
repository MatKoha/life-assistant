using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace life_assistant.Server.Controllers
{
    [ApiController]
    [Route("api/weather")]
    public class AccuWeatherController : ControllerBase
    {
        private readonly ILogger<AccuWeatherController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public AccuWeatherController(ILogger<AccuWeatherController> logger, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet("hourly/{locationId}")]
        public async Task<ActionResult<List<ApiForecast>>> GetHourlyWeather(int locationId)
        {
            return await GetWeatherData<List<HourlyWeatherForecastResponse>, ApiForecast>($"/forecasts/v1/hourly/12hour/{locationId}");
        }

        [HttpGet("daily/{locationId}")]
        public async Task<ActionResult<List<ApiDayWeather>>> GetDailyWeather(int locationId)
        {
            return await GetWeatherData<DailyWeatherForecastResponse, ApiDayWeather>($"/forecasts/v1/daily/5day/{locationId}");
        }

        private async Task<ActionResult<List<TOutput>>> GetWeatherData<TResponse, TOutput>(string endpoint)
        {
            try
            {
                string apiKey = _config["ACCUWEATHER_API_KEY"];
                string baseUrl = "http://dataservice.accuweather.com";

                var client = _httpClientFactory.CreateClient("AccuWeatherApi");

                var response = await client.GetAsync($"{baseUrl}{endpoint}?apikey={apiKey}&details=true&metric=true");
                var responseData = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    var apiItem = JsonConvert.DeserializeObject<TResponse>(responseData);
                    if (apiItem is List<HourlyWeatherForecastResponse> hourForecasts)
                    {
                        return Ok(hourForecasts.Select(forecast => new ApiForecast(forecast)).ToList() as List<TOutput>);
                    }
                    else if (apiItem is DailyWeatherForecastResponse dayForecasts)
                    {
                        return Ok(dayForecasts.DailyForecasts.Select(day => new ApiDayWeather(day)).ToList() as List<TOutput>);
                    }
                }

                var apiError = JsonConvert.DeserializeObject<ApiError>(responseData);
                return BadRequest($"{apiError.Code}: {apiError.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching weather data.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
