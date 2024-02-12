using Microsoft.AspNetCore.Mvc;

namespace life_assistant.Server.Controllers
{
    [ApiController]
    [Route("api")]
    public class UtilityController : ControllerBase
    {
        private readonly ILogger<HueController> _logger;
        private readonly IConfiguration _config;

        public UtilityController(ILogger<HueController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        #region Routes

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok();
        }

        #endregion
    }
}
