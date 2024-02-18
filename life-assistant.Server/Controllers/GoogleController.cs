using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Tasks.v1;
using Google;
using Newtonsoft.Json;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Util.Store;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Calendar.v3;
using Task = Google.Apis.Tasks.v1.Data.Task;
using life_assistant.Server.Classes;
using System.Net;

namespace life_assistant.Server.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleController : ControllerBase
    {
        private readonly ILogger<GoogleController> _logger;
        private readonly IConfiguration _config;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string tasklistId;
        private readonly string calendarId;

        public GoogleController(ILogger<GoogleController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            clientId = _config["GOOGLE_CLIENT_ID"];
            clientSecret = _config["GOOGLE_CLIENT_SECRET"];
            tasklistId = _config["GOOGLE_TASKLIST_ID"];
            calendarId = _config["GOOGLE_CALENDAR_ID"];
        }

        [HttpPost("authorize")]
        public async Task<ApiResponse<bool>> AuthorizePost()
        {
            if (!IsXmlHttpRequest())
            {
                return ApiResponse.Fail<bool>(HttpStatusCode.BadRequest, "Invalid X-Requested-With header.");
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
                    this.StoreGoogleToken(tokenResponse);

                    return ApiResponse.Success(true);
                }
            }

            return ApiResponse.Fail<bool>(HttpStatusCode.Unauthorized, "Authorization failed.");
        }

        private bool IsXmlHttpRequest()
        {
            return Request.Headers.TryGetValue("X-Requested-With", out var headerValues) &&
                   headerValues.Contains("XmlHttpRequest");
        }

        [HttpGet("calendar-events")]
        [GoogleTokenValidation]
        public async Task<ApiResponse<List<ApiGoogleCalendarEvent>>> GetCalendarEvents()
        {
            var (accessToken, refreshToken) = GoogleTokenValidationAttribute.GetTokensFromCookie(HttpContext);
            var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                Scopes = new string[] { CalendarService.Scope.CalendarReadonly },
                DataStore = new FileDataStore("Store")
            });

            var token = new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            var cred = new UserCredential(flow, "user", token);
            var service = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = cred,
            });

            // Define parameters of request.
            var request = service.Events.List(calendarId);
            request.TimeMin = DateTime.Now.Date;
            request.SingleEvents = true;
            request.MaxResults = 20;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;
            request.ShowDeleted = false;

            // List events.
            var events = await request.ExecuteAsync();

            if (events.Items == null || events.Items.Count == 0)
            {
                return ApiResponse.Success(new List<ApiGoogleCalendarEvent>());
            }

            var list = events.Items.Select(e => new ApiGoogleCalendarEvent(e)).ToList();
            return ApiResponse.Success(new List<ApiGoogleCalendarEvent>());
        }

        [HttpGet("tasks")]
        [GoogleTokenValidation]
        public ApiResponse<IList<Task>> GetTasks()
        {
            return PerformGoogleTasksApiAction<IList<Task>>(service =>
            {
                var request = service.Tasks.List(tasklistId);
                request.MaxResults = 20;
                request.ShowCompleted = true;
                var result = request.Execute();
                var orderedTasks = result.Items
                    .OrderBy(task => DateTime.Parse(task.Due))
                    .ThenBy(task => task.Title)
                    .ToList();

                return orderedTasks;
            });
        }

        [HttpPost("tasks")]
        [GoogleTokenValidation]
        public ApiResponse<Task> InsertTask([FromBody] Task task)
        {
            return PerformGoogleTasksApiAction(service =>
            {
                var result = service.Tasks.Insert(task, tasklistId).Execute();
                return result;
            });
        }

        [HttpPut("tasks")]
        [GoogleTokenValidation]
        public ApiResponse<Task> UpdateTask([FromBody] Task task)
        {
            return PerformGoogleTasksApiAction(service =>
            {
                var result = service.Tasks.Update(task, tasklistId, task.Id).Execute();
                return result;
            });
        }

        [HttpDelete("tasks/{id}")]
        [GoogleTokenValidation]
        public ApiResponse<string> DeleteTask(string id)
        {
            return PerformGoogleTasksApiAction(service =>
            {
                var result = service.Tasks.Delete(tasklistId, id).Execute();
                return $"Deleted {id}";
            });
        }

        private ApiResponse<T> PerformGoogleTasksApiAction<T>(Func<TasksService, T> action)
        {
            try
            {
                var (accessToken, refreshToken) = GoogleTokenValidationAttribute.GetTokensFromCookie(HttpContext);
                var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = clientId,
                        ClientSecret = clientSecret
                    },
                    Scopes = new string[] { TasksService.Scope.Tasks, TasksService.Scope.TasksReadonly },
                    DataStore = new FileDataStore("Store")
                });

                var token = new TokenResponse
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                var cred = new UserCredential(flow, "user", token);
                var service = new TasksService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = cred,
                });

                return ApiResponse.Success(action(service));
            }
            catch (GoogleApiException ex)
            {
                return ApiResponse.Fail<T>(ex.HttpStatusCode, $"Google API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                
                return ApiResponse.Fail<T>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
            }
        }

        private void StoreGoogleToken(GoogleAccessToken token)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddSeconds(token.ExpiresIn)
            };

            Response.Cookies.Append("GoogleAccessToken", token.AccessToken, cookieOptions);
            Response.Cookies.Append("GoogleRefreshToken", token.RefreshToken, cookieOptions);
        }
    }
}