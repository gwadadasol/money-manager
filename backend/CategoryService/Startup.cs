using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CategoryService.AsyncDataServices;
using CategoryService.Domains.Model;
using CategoryService.Domains.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace CategoryService
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            bool useAzure = bool.Parse(Configuration["UseAzure"]);
            bool useMemDb = bool.Parse(Configuration["useMemDb"]);

            Console.WriteLine(useAzure);

            if (_env.IsDevelopment())
            {
                Console.WriteLine("Development Mode");

                if (useAzure)
                {
                    Console.WriteLine("Use Azure");
                    string conString = Configuration.GetConnectionString("SqlServer");
                    var conStrBuilder = new SqlConnectionStringBuilder(conString);

                    string secret = "";
                    try
                    {
                        var vaultUri = new Uri("https://moneymanagervaultdev.vault.azure.net/");
                        SecretClient secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());

                        secret = secretClient.GetSecret("AzureSQLPasswordDev").Value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Console.WriteLine($"secret: {secret}");

                    try
                    {
                        var vaultUri2 = new Uri("https://moneymanagervault2dev.vault.azure.net/");
                        SecretClient secretClient2 = new SecretClient(vaultUri2, new DefaultAzureCredential());

                        secret = secretClient2.GetSecret("AzureSQLPasswordDev").Value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine($"secret2: {secret}");
                    Console.WriteLine($"secret: {secret}");

                    var connection = conStrBuilder.ConnectionString;
                    services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));

                }
                else
                {
                    if (useMemDb)
                    {
                        Console.WriteLine("Use In Memory DB");
                        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
                    }
                    else
                    {
                        Console.WriteLine("Use Local DB");
                        var conStrBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SqlServerLocal"));
                        conStrBuilder.Password = Configuration["DB_PASSWORD"];
                        conStrBuilder.UserID = Configuration["DB_USER"];

                        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conStrBuilder.ConnectionString));
                    }
                }

            }
            else
            {
                Console.WriteLine(">>>>>>>>>>>>>> Production Mode");
                if (useAzure)
                {
                    Console.WriteLine("Use Azure");

                    var conStrBuilder = new SqlConnectionStringBuilder(
                        Configuration.GetConnectionString("SqlServer")
                        );

                    string secret = "";
                    try
                    {
                        var vaultUri = new Uri("https://moneymanagervault2dev.vault.azure.net/");

                        SecretClient secretClient2 = new SecretClient(vaultUri, new DefaultAzureCredential());
                        secret = secretClient2.GetSecretAsync("AzureSQLPassword").Result.Value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("excption for https://moneymanagervault2dev.vault.azure.net/");
                        Console.WriteLine(ex.Message);
                    }

                    if (string.IsNullOrEmpty(secret))
                    {
                        secret = Configuration["AZURE_SQL_PASSWORD"];
                    }

                    Console.WriteLine($"---------------------- secret2: {secret}");
                    conStrBuilder.Password = secret;

                    var connection = conStrBuilder.ConnectionString;
                    services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));
                }
                else
                {
                    if (useMemDb)
                    {
                        var conStrBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SqlServer"));
                        conStrBuilder.Password = Configuration["AzureSQLPassword"];
                        var connection = conStrBuilder.ConnectionString;
                        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));
                    }
                    else
                    {
                        Console.WriteLine("Use Local DB");
                        var conStrBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SqlServerLocal"));
                        conStrBuilder.Password = Configuration["DB_PASSWORD"];
                        conStrBuilder.UserID = Configuration["DB_USER"];
                        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conStrBuilder.ConnectionString));
                    }
                }
            }

            services.AddMediatR(typeof(Startup));
            services.AddControllers();

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IMessageBusClient, MessageBusClient>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CategoryService", Version = "v1" });
            });

            services.AddCors(options => options.AddDefaultPolicy(
              builder =>
              {
                  builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
              }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            SwaggerOptions swaggerOptions = new();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CategoryService v1"));

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
