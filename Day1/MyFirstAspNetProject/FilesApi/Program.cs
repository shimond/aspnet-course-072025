using FilesApi.Contracts;
using FilesApi.Middlewares;
using FilesApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFileManagerService, FileManagerService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<AspNetCourseHeaderMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
