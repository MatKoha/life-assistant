using life_assistant.Server.Classes.Hue;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace life_assistant.Server.Controllers
{
    [ApiController]
    [Route("api/hue")]
    public class HueController : ControllerBase
    {
        private readonly ILogger<HueController> _logger;
        private readonly X509Certificate2 _caCertificate;
        private readonly IConfiguration _config;
        private readonly string _bridgeAddress;

        public HueController(ILogger<HueController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _bridgeAddress = _config["HUE_BRIDGE_ADDRESS"];

            string certText = Uri.UnescapeDataString(_config["HUE_CERT"]);
            byte[] certificateData = Convert.FromBase64String(certText);
            _caCertificate = new X509Certificate2(certificateData);
        }

        #region Routes

        [HttpGet("devices")]
        public async Task<IActionResult> GetDevices()
        {
            string url = $"https://{_bridgeAddress}/clip/v2/resource/device";
            return await MakeRequestAsync(HttpMethod.Get, url);
        }

        [HttpPut("home-state")]
        public async Task<IActionResult> SetHomeState([FromBody] ApiHueHomeState request)
        {
            string url = $"https://{_bridgeAddress}/clip/v2/resource/grouped_light/{_config["HOME_HOME_ID"]}";
            return await MakeRequestAsync(HttpMethod.Put, url, new { on = new { on = request.LightState } });
        }

        #endregion

        #region Private Methods

        private HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = ValidateServerCertificate;

            var httpClient = new HttpClient(handler);
            httpClient.DefaultRequestHeaders.Add("hue-application-key", _config["HUE_USERNAME"]);

            return httpClient;
        }

        private async Task<IActionResult> MakeRequestAsync(HttpMethod method, string url, object requestBody = null)
        {
            try
            {
                using (var httpClient = CreateHttpClient())
                {
                    HttpContent httpContent = null;
                    if (requestBody != null)
                    {
                        var json = JsonConvert.SerializeObject(requestBody);
                        httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    }

                    HttpResponseMessage response = null;

                    if (method == HttpMethod.Get)
                    {
                        response = await httpClient.GetAsync(url);
                    }
                    else if (method == HttpMethod.Put)
                    {
                        response = await httpClient.PutAsync(url, httpContent);
                    }
                    else
                    {
                        return BadRequest("Unsupported HTTP method");
                    }

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return Ok(responseData);
                    }
                    else
                    {
                        return BadRequest(response);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while making the {method} request.");
                return StatusCode(500, ex.Message);
            }
        }

        private bool ValidateServerCertificate(HttpRequestMessage requestMessage, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            if (chain.ChainStatus.Length > 0 && chain.ChainStatus[0].Status == X509ChainStatusFlags.UntrustedRoot)
            {
                if (certificate.Subject == _caCertificate.Issuer)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
