using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class GoogleTokenValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var httpContext = context.HttpContext;
        var (accessToken, refreshToken) = GetTokensFromCookie(httpContext);

        if (string.IsNullOrEmpty(accessToken))
        {
            context.Result = new UnauthorizedObjectResult("Token expired.");
            return;
        }

        base.OnActionExecuting(context);
    }

    public static (string? accessToken, string? refreshToken) GetTokensFromCookie(HttpContext httpContext)
    {
        return (httpContext.Request.Cookies["GoogleAccessToken"], httpContext.Request.Cookies["GoogleRefreshToken"]);
    }
}
