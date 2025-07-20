using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MyStore.Api.Api;
using MyStore.Api.Contracts;
using MyStore.Api.Data;
using MyStore.Api.Exceptions;
using MyStore.Api.Services;
using System.Reflection;

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

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{

    //var dbContext = app.Services.GetRequiredService<MyStoreDataContext>(); // error: Cannot access without scope.
    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<MyStoreDataContext>();
    await dbContext.Database.EnsureCreatedAsync(); 
    app.MapOpenApi();
}


app.Use(async (context, next) => {

	try
	{
        await next();
    }
	catch (ItemNotFoundException ex)
	{
        app.Logger.LogError(ex,"Item not found");
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync(ex.Message);
	}
});


app.UseHttpsRedirection();

app.MapProductApis();

app.Run();
