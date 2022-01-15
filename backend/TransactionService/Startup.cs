using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TransactionService.AsyncDataServices;
using TransactionService.Domains.Model;
using TransactionService.Domains.Repository;
using TransactionService.EventProcessing;
using TransactionService.Options;

namespace TransactionService
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
            // services.InstallServicesAssembly(Configuration);
            bool useAzure = bool.Parse(Configuration["UseAzure"]);
            Console.WriteLine(useAzure);


            if (_env.IsDevelopment())
            {
                Console.WriteLine("Development Mode");
                Console.WriteLine("Use In Memory DB");
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
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

                    var conStrBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SqlServer"));
                    conStrBuilder.Password = Configuration["AzureSQLPassword"];
                    var connection = conStrBuilder.ConnectionString;
                    services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));
                }
            }

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddMediatR(typeof(Startup));
            services.AddControllers();

            services.AddHostedService<MessageBusSubscriber>();
            
            services.AddSingleton<IEventProcessor, EventProcessor>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen();

            services.AddCors(options => options.AddDefaultPolicy(
                //name: "MyPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
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
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);

            // app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description));

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
