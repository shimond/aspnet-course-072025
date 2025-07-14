using Api.Model.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ServerInfoConfig>(builder.Configuration.GetSection("ServerInfo"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();


app.Run();

