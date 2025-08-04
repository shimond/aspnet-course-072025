var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.AddHttpClient("productsApi", x => {
    x.BaseAddress = new Uri("http://mystore.api:8080");
});

// Add YARP Reverse Proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddServiceDiscoveryDestinationResolver();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Map YARP Reverse Proxy
app.MapReverseProxy();

app.MapGet("test", () => "WOW");

//app.MapGet("/api/products", async (IHttpClientFactory factory) => {
//    var client = factory.CreateClient("productsApi");
//    var result = await client.GetStringAsync("/products");
//    return TypedResults.Ok(result);
//});

app.Run();

