using System.Reflection;
using AnalyzerService.Domains.Model;
using AnalyzerService.Domains.Repository;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

var conStrBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("SqlServerLocal"));
conStrBuilder.Password = configuration["DB_PASSWORD"];
conStrBuilder.UserID = configuration["DB_USER"];

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conStrBuilder.ConnectionString));

// Add services to the container.

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly()); TODO: What is the difference?

builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddDefaultPolicy(
                //name: "MyPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                }));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
