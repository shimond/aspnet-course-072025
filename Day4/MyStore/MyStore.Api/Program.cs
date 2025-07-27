
using MyStore.Api.Auth;
using MyStore.Api.Middlewares;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<IApplicationMapper, ApplicationMapper>();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<MyStoreDataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("MyStoreDB")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddCors(x =>
    x.AddDefaultPolicy(o => o.WithOrigins("https://shimonclient.com")
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod()));

builder.Services.AddAuthorization(authBuilder => {
    authBuilder.AddPolicy(AuthorizationPolicies.ADMIN_POLICY, apb => {
        apb.RequireRole("Admin");
        //apb.RequireClaim("scope:delete-product", "1");
    });

});



builder.Services.AddAuthentication("Bearer").AddJwtBearer(); // add the authentication scheme

var app = builder.Build();

app.UseCors(); // listen to CORS requests with OPTIONS preflight requests   
app.UseHandleApplicationErrorMiddleware();

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



// using configuration
// using dependency injection
// using fluent validation
// using automapper - (dto to dto mapping)
// custom exception handling (with middleware)
// using efcore with sql server (with in-memory database for development)
// using cors