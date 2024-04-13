using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CustomerAPP.Helper
{
    public class ApiKeyAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
            {
                //allow anonyms Controller's method
                await _next(context);
                return;
            }
            var apiKey = context.Session.GetString("APITokenDetails");

            if (!string.IsNullOrEmpty(apiKey))
            {
                await _next(context);
                return;
            }
            else
            {
                //return Unauthorized if Token is not available
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                
            }

        }


    }
}
