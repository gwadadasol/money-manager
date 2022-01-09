using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddOcelot();

builder.Configuration
.SetBasePath(builder.Environment.ContentRootPath)
.AddJsonFile("appsettings.json",true)
.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
// .AddJsonFile("ocelot.json");
.AddOcelot("Routes",  builder.Environment);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseOcelot().Wait();

app.MapGet("/", () => "Hello World!");

app.Run();
