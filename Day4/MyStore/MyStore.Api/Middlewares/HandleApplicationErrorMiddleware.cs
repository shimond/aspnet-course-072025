using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyStore.Api.Exceptions;
using System.Threading.Tasks;

namespace MyStore.Api.Middlewares
{
    public class HandleApplicationErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public HandleApplicationErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (ItemNotFoundException ex)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync(ex.Message);
            }

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HandleApplicationErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseHandleApplicationErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandleApplicationErrorMiddleware>();
        }
    }
}
