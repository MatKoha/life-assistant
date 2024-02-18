using life_assistant.Server.Classes;
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
        public async Task<ApiResponse<List<ApiForecast>>> GetHourlyWeather(int locationId)
        {
            var response = await GetWeatherData($"/forecasts/v1/hourly/12hour/{locationId}");
            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse.Fail<List<ApiForecast>>(response.StatusCode, response.ReasonPhrase);
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<HourlyWeatherForecastResponse>>(responseData);
            var list = data.Select(x => new ApiForecast(x)).ToList();

            return ApiResponse.Success(list);
        }

        [HttpGet("daily/{locationId}")]
        public async Task<ApiResponse<List<ApiDayWeather>>> GetDailyWeather(int locationId)
        {
            var response = await GetWeatherData($"/forecasts/v1/daily/5day/{locationId}");
            if (!response.IsSuccessStatusCode)
            {
                return ApiResponse.Fail<List<ApiDayWeather>>(response.StatusCode, response.ReasonPhrase);
            }

            var responseData = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<DailyWeatherForecastResponse>(responseData);
            var list = data.DailyForecasts.Select(x => new ApiDayWeather(x)).ToList();
            return ApiResponse.Success(list);
        }

        private async Task<HttpResponseMessage> GetWeatherData(string endpoint)
        {
            string apiKey = _config["ACCUWEATHER_API_KEY"];
            string baseUrl = "http://dataservice.accuweather.com";

            var client = _httpClientFactory.CreateClient("AccuWeatherApi");

            var response = await client.GetAsync($"{baseUrl}{endpoint}?apikey={apiKey}&details=true&metric=true");
            return response;
        }
    }
}
