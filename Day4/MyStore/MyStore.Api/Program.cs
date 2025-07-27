using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyStore.Api.Api;
using MyStore.Api.Contracts;
using MyStore.Api.Data;
using MyStore.Api.EndpointFilters;
using MyStore.Api.Exceptions;
using MyStore.Api.Services;
using System.Reflection;


// using configuration
// using dependency injection
// using fluent validation
// using automapper - (dto to dto mapping)
// custom exception handling (with middleware)
// using efcore with sql server (with in-memory database for development)
// using cors

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IApplicationMapper, ApplicationMapper>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddDbContext<MyStoreDataContext>(o => o.UseInMemoryDatabase("MyStoreDb"));
builder.Services.AddDbContext<MyStoreDataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyStoreDB")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddCors(x =>
x.AddDefaultPolicy(o => o.WithOrigins("https://shimonclient.com")
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod()));



builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer").AddJwtBearer(); // add the authentication scheme

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{

    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<MyStoreDataContext>();
    await dbContext.Database.EnsureCreatedAsync();
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();

}
app.UseAuthentication();
app.UseAuthorization();

var all = app.MapGroup("").AddEndpointFilter<ValidationEndPointFilter>();

all.MapProductApis();

app.Run();
