using life_assistant.Server.Classes;
using life_assistant.Server.Classes.Hue;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace life_assistant.Server.Controllers
{
    [ApiController]
    [Route("api/hue")]
    public class HueController : ControllerBase
    {
        private readonly ILogger<HueController> _logger;
        private readonly IConfiguration _config;
        private readonly string _apiUrl = "https://api.meethue.com/route/clip/v2";

        public HueController(ILogger<HueController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        #region Routes

        [HueTokenValidation]
        [HttpGet("devices")]
        public async Task<IActionResult> GetDevices()
        {
            var result = await GetHueResource("device");
            return Ok();
        }

        [HueTokenValidation]
        [HttpGet("lights")]
        public async Task<ApiResponse<ApiHueLight>> GetLights()
        {
            var result = await GetHueResource("light");
            if (!result.IsSuccessStatusCode)
            {
                return ApiResponse.Fail<ApiHueLight>(result.StatusCode, result.ReasonPhrase);
            }

            var content = await result.Content.ReadAsStringAsync();
            return ApiResponse.Success<ApiHueLight>(content);
        }

        [HueTokenValidation]
        [HttpPut("home-state")]
        public async Task<IActionResult> TurnOffLights()
        {
            var result = await GetHueResource($"grouped_light/{_config["HUE_HOME_ID"]}");
            return Ok();
        }

        [HttpPost("token/{code}")]
        public async Task<IActionResult> GetToken(string code)
        {
            var clientId = _config["HUE_CLIENT_ID"];
            var clientSecret = _config["HUE_CLIENT_SECRET"];
            string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            string apiUrl = "https://api.meethue.com/v2/oauth2/token";
            var content = new StringContent($"grant_type=authorization_code&code={code}", Encoding.UTF8, "application/x-www-form-urlencoded");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64Credentials}");

                var response = await client.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var token = JsonConvert.DeserializeObject<HueToken>(responseData);
                    this.StoreHueToken(token);
                    return await UpdateHueConfig(token.AccessToken);
                }
                else
                {
                    return BadRequest(response);
                }
            }
        }

        public class HueApiSuccessResponse
        {
            [JsonProperty("success")]
            public SuccessData Success { get; set; }
        }

        public class SuccessData
        {
            [JsonProperty("username")]
            public string Username { get; set; }
        }


        public class HueToken
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }

            [JsonProperty("refresh_token")]
            public string RefreshToken { get; set; }

            [JsonProperty("token_type")]
            public string TokenType { get; set; }
        }

        #endregion

        #region Private Methods

        private async Task<IActionResult> UpdateHueConfig(string accessToken)
        {
            string apiUrl = "https://api.meethue.com/route/api/0/config";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                string jsonPayload = "{\"linkbutton\":true}";
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return await GenerateApplicationKey(accessToken);
                }
            }

            return BadRequest("Could not update Hue config");
        }

        private async Task<IActionResult> GenerateApplicationKey(string accessToken)
        {
            string apiUrl = "https://api.meethue.com/route/api";
            string applicationName = _config["HUE_APP_NAME"];

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                string jsonPayload = $"{{\"devicetype\":\"{applicationName}\"}}";
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var keyResults = JsonConvert.DeserializeObject<List<HueApiSuccessResponse>>(responseData);
                    HttpContext.Session.SetString("HueApplicationKey", keyResults.First().Success.Username);
                    CookieOptions cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTime.MaxValue
                    };

                    Response.Cookies.Append("HueApplicationKey", keyResults.First().Success.Username, cookieOptions);

                    return Ok();
                }

                return BadRequest("Could not generate Hue app key");
            }
        }

        private async Task<IActionResult> RefreshToken()
        {
            var clientId = _config["HUE_CLIENT_ID"];
            var clientSecret = _config["HUE_CLIENT_SECRET"];
            var token = Request.Cookies["HueRefreshToken"];

            string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            using (HttpClient client = new HttpClient())
            {
                var url = "https://api.meethue.com/v2/oauth2/token";
                client.DefaultRequestHeaders.Add("Authorization", $"Basic {base64Credentials}");

                var requestBody = $"grant_type=refresh_token&refresh_token={token}";
                var content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var newToken = JsonConvert.DeserializeObject<HueToken>(responseData);
                    this.StoreHueToken(newToken);

                    return Ok();
                }

                return Unauthorized();
            }
        }

        private async Task<HttpResponseMessage> GetHueResource(string resourcePath)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {Request.Cookies["HueAccessToken"]}");
                client.DefaultRequestHeaders.Add("hue-application-key", Request.Cookies["HueApplicationKey"]);

                var response = await client.GetAsync(_apiUrl + $"/resource/{resourcePath}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    await RefreshToken();
                    response = await client.GetAsync(_apiUrl + resourcePath);
                }

                return response;
            }
        }

        private void StoreHueToken(HueToken token)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddSeconds(token.ExpiresIn)
            };

            Response.Cookies.Append("HueAccessToken", token.AccessToken, cookieOptions);
            Response.Cookies.Append("HueRefreshToken", token.RefreshToken, cookieOptions);
        }

        #endregion
    }
}
