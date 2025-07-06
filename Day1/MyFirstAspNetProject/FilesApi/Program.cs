var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.Use(async (context, next) => {
    context.Request.Headers.TryGetValue("aspnetcourse", out var headerValue);
    if (string.IsNullOrEmpty(headerValue) || headerValue != "06072025")
    { 
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("Unauthorized access");
        return;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
