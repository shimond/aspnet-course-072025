using Api.Contracts;
using Api.Middlewares;
using Api.Model.Config;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TenantConfig>(builder.Configuration.GetSection("TenantSettings"));
builder.Services.AddScoped<ITenantContextAccessor, TenantContextAccessor>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();
app.UseTenantValidationMiddleware();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.Run();
