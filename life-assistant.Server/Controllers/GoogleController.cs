using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Tasks.v1;
using Google;
using System.Text;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [ApiController]
    [Route("api/google")]
    public class GoogleController : ControllerBase
    {
        private readonly ILogger<GoogleController> _logger;

        public GoogleController(ILogger<GoogleController> logger)
        {
            _logger = logger;
        }

        [GoogleScopedAuthorize(TasksService.ScopeConstants.TasksReadonly)]
        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks([FromServices] IGoogleAuthProvider auth)
        {
            if (await auth.RequireScopesAsync(TasksService.Scope.TasksReadonly) is IActionResult authResult)
            {
                return authResult;
            }

            try
            {
                GoogleCredential cred = await auth.GetCredentialAsync();
                var service = new TasksService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = cred,
                });

                var request = service.Tasks.List(Environment.GetEnvironmentVariable("GoogleTaskListId"));
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