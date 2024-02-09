using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class TokenValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var (accessToken, refreshToken) = GetTokensFromSession(httpContext);

        if (string.IsNullOrEmpty(accessToken))
        {
            context.Result = new UnauthorizedObjectResult("Token expired.");
            return;
        }

        base.OnActionExecuting(context);
    }

    public static (string? accessToken, string? refreshToken) GetTokensFromSession(HttpContext httpContext)
    {
        return (httpContext.Session.GetString("GoogleAccessToken"), httpContext.Session.GetString("GoogleRefreshToken"));
    }
}
