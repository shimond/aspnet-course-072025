using Api.Contracts;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
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


// Configure the HTTP request pipeline.
//app.Use(async (context, next) =>
//{
//    try
//    {
//        await next();
//    }
//    catch (Exception ex)
//    {
//        app.Logger.LogError(ex, "Application error");
//    }
//});

app.UseOutputCache();
app.UseStaticFiles();
app.MapControllers();

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync(" My First mw  1 "); // 1
//    await next();
//    await context.Response.WriteAsync(" My First mw  2 "); // 5
//});


//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync(" My Second mw 1 "); // 2
//    await next();
//    await context.Response.WriteAsync(" My Second mw 2 "); // 4
//});

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync(" Last One "); // 3
//});


app.Run();

//class ShimonCacheStore : IOutputCacheStore
//{
//    public ValueTask EvictByTagAsync(string tag, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    public ValueTask<byte[]?> GetAsync(string key, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }

//    public ValueTask SetAsync(string key, byte[] value, string[]? tags, TimeSpan validFor, CancellationToken cancellationToken)
//    {
//        throw new NotImplementedException();
//    }
//}