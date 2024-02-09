using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Tasks.v1;
using Google;
using System.Text;
using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Flows;

namespace server.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleController : ControllerBase
    {
        private readonly ILogger<GoogleController> _logger;
        private readonly IConfiguration _config;
        private readonly string clientId;
        private readonly string clientSecret;

        public GoogleController(ILogger<GoogleController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            clientId = _config["GOOGLE_CLIENT_ID"];
            clientSecret = _config["GOOGLE_CLIENT_SECRET"];
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> AuthorizePost()
        {
            if (!IsXmlHttpRequest())
            {
                return BadRequest("Invalid X-Requested-With header.");
            }

            var authorizationCode = Request.Form["code"];

            using (var httpClient = new HttpClient())
            {
                var tokenEndpoint = "https://oauth2.googleapis.com/token";
                var parameters = new Dictionary<string, string>
                {
                    {"client_id", clientId},
                    {"client_secret", clientSecret},
                    {"code", authorizationCode},
                    {"grant_type", "authorization_code"},
                    {"redirect_uri", _config["GOOGLE_REDIRECT_URI"]}
                };

                var content = new FormUrlEncodedContent(parameters);

                var response = await httpClient.PostAsync(tokenEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResult = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<GoogleAccessToken>(jsonResult);
                    HttpContext.Session.SetString("GoogleAccessToken", tokenResponse.AccessToken);
                    HttpContext.Session.SetString("GoogleRefreshToken", tokenResponse.RefreshToken);

                    return Ok();
                }
            }

            return BadRequest("Authorization failed.");
        }

        private bool IsXmlHttpRequest()
        {
            return Request.Headers.TryGetValue("X-Requested-With", out var headerValues) &&
                   headerValues.Contains("XmlHttpRequest");
        }

        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            string accessToken = HttpContext.Session.GetString("GoogleAccessToken");
            string refreshToken = HttpContext.Session.GetString("GoogleRefreshToken");

            if (string.IsNullOrEmpty(accessToken))
            {
                return Unauthorized("Token expired.");
            }

            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = new string[] { TasksService.Scope.TasksReadonly },
                DataStore = new FileDataStore("Store")
            });

            var token = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            try
            {
                var cred = new UserCredential(flow, "user", token);
                var service = new TasksService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = cred,
                });

                var request = service.Tasks.List(_config["GOOGLE_TASKLIST_ID"]);
                request.MaxResults = 20;
                request.ShowCompleted = true;
                var result = request.Execute();
                var orderedTasks = result.Items
                    .OrderBy(task => DateTime.Parse(task.Due))
                    .ToList();

                return Ok(orderedTasks);
            }
            catch (GoogleApiException ex)
            {
                return BadRequest($"Google API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}