using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FilesApi.Middlewares;

public class HeaderValidationMiddleware
{
    private readonly RequestDelegate _next;

    public HeaderValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        httpContext.Request.Headers.TryGetValue("aspnetcourse", out var headerValue);
        if (string.IsNullOrEmpty(headerValue) || headerValue != "06072025")
        {
            httpContext.Response.StatusCode = 401; // Unauthorized
            await httpContext.Response.WriteAsync("Unauthorized access");
        }
        else { 
            await _next(httpContext);
        }
    }
}

public static class HeaderValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseHeaderValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HeaderValidationMiddleware>();
    }
}
