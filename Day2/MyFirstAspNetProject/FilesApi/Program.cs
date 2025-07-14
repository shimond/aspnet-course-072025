using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFileManagerService, FileManagerService>();
builder.Services.AddControllers();
// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Files API", Version = "v1" });
});

var app = builder.Build();

app.Use(async (context, next) => {

	try
	{
		await next();
	}
	catch (Exception ex)
	{
		throw;
	}
});

// Enable Swagger middleware for all environments

if (app.Environment.IsDevelopment())
{
    // register OpenAPI endpoints by default  = openapi/v1.json

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Files API v1");
    });
}

app.MapControllers();

app.Run();


// 200, 201, 202, 204 - OK
// 400, 401, 403, 404, 409 - Client error
// 500, 501, 502, 503 - Server error















































