using life_assistant.Server.Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using server.Classes;
using System.Net.Http.Headers;
using System.Text;

namespace life_assistant.Server.Controllers
{
    [ApiController]
    [Route("api/polar")]
    public class PolarController : ControllerBase
    {
        private readonly ILogger<PolarController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DataDbContext db;
        private readonly IConfiguration _config;

        public PolarController(DataDbContext db, ILogger<PolarController> logger, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            this.db = db;
            _config = config;
        }

        [HttpPost("users/{code}")]
        public async Task<IActionResult> UpdatePolarUserByCode(string code)
        {
            try
            {
                var token = await RequestToken(code);

                if (token == null)
                {
                    return BadRequest("Could not request token.");
                }

                var dbUser = this.db.PolarUsers.FirstOrDefault(x => x.MemberId == token.MemberId);
                if (dbUser == null)
                {
                    var registered = await this.TryRegisterUser(token.Token, token.MemberId);
                    if (!registered)
                    {
                        return BadRequest("Could not register user.");
                    }

                    dbUser = new PolarUser { MemberId = token.MemberId, AccessToken = token.Token };
                    this.db.PolarUsers.Add(dbUser);
                }

                var polarUser = await GetPolarUser(token.Token, token.MemberId);
                if (polarUser == null)
                {
                    return BadRequest("Could not get user data from Polar.");
                }

                dbUser.FirstName = polarUser.FirstName;
                dbUser.LastName = polarUser.LastName;

                this.db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("users/{memberId}/sleep")]
        public async Task<ApiResponse<SleepData>> GetSleepData(int memberId)
        {
            var polarUser = this.db.PolarUsers.FirstOrDefault(x => x.MemberId == memberId);

            if (polarUser == null)
            {
                return ApiResponse.Fail<SleepData>(System.Net.HttpStatusCode.NotFound, "User not found.");
            }

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", polarUser.AccessToken);

                var response = await httpClient.GetAsync($"https://www.polaraccesslink.com/v3/users/sleep");
                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponse.Fail<SleepData>(response.StatusCode, response.ReasonPhrase);
                }

                var responseData = await response.Content.ReadAsStringAsync();
                return ApiResponse.Success<SleepData>(responseData);
            }
        }

        private async Task<PolarToken?> RequestToken(string authCode)
        {
            string clientId = _config["POLAR_CLIENT_ID"];
            string clientSecret = _config["POLAR_CLIENT_SECRET"];

            string combinedCredentials = $"{clientId}:{clientSecret}";

            string encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(combinedCredentials));
            var inputBody = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", authCode)
            });

            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);

                var response = await httpClient.PostAsync("https://polarremote.com/v2/oauth2/token", inputBody);
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<PolarToken>(responseData);

                return result;
            }
        }

        private async Task<ApiPolarUser?> GetPolarUser(string accessToken, int memberId)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync($"https://www.polaraccesslink.com/v3/users/{memberId}");
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<ApiPolarUser>(responseData);
                }

                return null;
            }
        }

        private async Task<bool> TryRegisterUser(string accessToken, int memberId)
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var json = new JObject { { "member-id", memberId } };

                var content = new StringContent(JsonConvert.SerializeObject(json.ToString()), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://www.polaraccesslink.com/v3/users", content);

                return response.IsSuccessStatusCode;
            }
        }
    }
}
