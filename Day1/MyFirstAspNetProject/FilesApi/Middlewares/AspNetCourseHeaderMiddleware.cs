namespace FilesApi.Middlewares;

public class AspNetCourseHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public AspNetCourseHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue("aspnetcourse", out var headerValue);
        // read header name and value from configuration/env variable or secrets
        if (string.IsNullOrEmpty(headerValue) || headerValue != "06072025")
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Unauthorized access");
            return;
        }
        await _next(context);
    }
}
