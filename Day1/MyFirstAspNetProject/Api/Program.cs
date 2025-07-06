using Api.Contracts;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddOutputCache(); // save cache in memory

// lifetime of the service
//builder.Services.AddSingleton<IGeneratorService, GeneratorImplementationService>(); // one for all
builder.Services.AddScoped<IGeneratorService, GeneratorImplementationService>(); // by default per http request
//builder.Services.AddTransient<IGeneratorService, GeneratorImplementationService>(); // each time it require

var app = builder.Build();

app.Use(async (context, next) => {
    var r = context.RequestServices.GetRequiredService<IGeneratorService>();
    await next();
    app.Logger.LogInformation(r.GenerateString(200));
});


app.UseOutputCache();
app.UseStaticFiles();
app.MapControllers();


app.Run();

