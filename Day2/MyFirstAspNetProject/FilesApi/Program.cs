var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFileManagerService, FileManagerService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

app.Use(async (context, next) => {

	try
	{
		await next();
	}
	catch (Exception ex)
	{
		throw;
	}
});
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // register OpenAPI endpoints by default  = openapi/v1.json
}
//app.UseMiddleware<AspNetCourseHeaderMiddleware>();

//app.Use(async (context, next) => {

//    await next();
//});

app.MapControllers();

app.Run();


// 200, 201, 202, 204 - OK
// 400, 401, 403, 404, 409 - Client error
// 500, 501, 502, 503 - Server error

Person po = new Person { Age = null };

class Person
{
	public string Name { get; set; }
	public int? Age { get; set; }
	public string? Address { get; set; }
}


// primitive types  (Stack )- Complex types (Heap) = pointers























