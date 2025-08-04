var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddHttpClient("productsApi", x => {
    x.BaseAddress = new Uri("http://mystore.api:8080");
});

// Add YARP Reverse Proxy
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

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

