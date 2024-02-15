using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class HueTokenValidationAttribute : ActionFilterAttribute
{
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var key = context.HttpContext.Request.Cookies["HueApplicationKey"];
            var token = context.HttpContext.Request.Cookies["HueAccessToken"];

            if (key == null || token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
}
