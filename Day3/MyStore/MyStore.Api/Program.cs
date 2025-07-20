using Microsoft.EntityFrameworkCore;
using MyStore.Api.Contracts;
using MyStore.Api.Data;
using MyStore.Api.Services;
using MyStore.Api.Api; // Add this for ProductsApi extension

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddDbContext<MyStoreDataContext>(o => o.UseInMemoryDatabase("MyStoreDb"));
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapProductApis();

app.Run();
