using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace life_assistant.Server.Classes
{
    public abstract class ApiResponse : IActionResult
    {
        public static ApiResponse<T> Success<T>(string jsonData)
        {
            var data = JsonConvert.DeserializeObject<T>(jsonData);
            return new ApiResponse<T>(data);
        }

        public static ApiResponse<T> Fail<T>(HttpStatusCode errorCode, string errorMessage)
        {
            var response = new ApiResponse<T>(default, errorCode, errorMessage);

            return response;
        }

        public abstract Task ExecuteResultAsync(ActionContext context);
    }

    public class ApiResponse<T> : ApiResponse
    {
        public readonly T? Result;
        public readonly HttpStatusCode Code;
        public readonly string? Message;


        public ApiResponse(T result, HttpStatusCode code = HttpStatusCode.OK, string? message = null)
        {
            this.Result = result;
            this.Code = code;
            this.Message = message;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = new ObjectResult(this.Result) { StatusCode = (int)this.Code };
            if (this.Result == null || this.Result.Equals(default(T)) == true)
            {
                response.Value = $"{(int)this.Code}: {this.Message}";
            }

            return response.ExecuteResultAsync(context);
        }
}
}
