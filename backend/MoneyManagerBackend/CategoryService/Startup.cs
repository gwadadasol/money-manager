using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using CategoryService.AsyncDataServices;
using CategoryService.Domains.Model;
using CategoryService.Domains.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
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
            Console.WriteLine(useAzure);

            if (_env.IsDevelopment())
            {
                Console.WriteLine("Development Mode");



                if (useAzure)
                {
                    Console.WriteLine("Use Azure");
                    string conString = Configuration.GetConnectionString("SqlServer");
                    var conStrBuilder = new SqlConnectionStringBuilder(conString);


                    // moneymanagerbackendappdev
                    //Display name : moneymanagerbackendappdev
                    // Application(client) ID :"d61b6013-0d9b-498b-b668-2b2d76cfdea0"
                    // Object ID : 788836b4 - 0c29 - 4ac4 - b694 - 718a8f46c4d1
                    // Directory(tenant) ID: d49f99a7 - 4e90 - 4a82 - a859 - 603329bbc7c0
                    // secretId : 3e633b07-fa51-46ea-a208-907ff214e7ea


                    //var stageOneConfig = configBuilder.Build();
                    //var clientId = stageOneConfig.GetValue<string>("clientid");
                    //var clientSecret = stageOneConfig.GetValue<string>("clientsecret");
                    //var keyVaultIdentifier = stageOneConfig.GetValue<string>("keyvaultidentifier");
                    //var keyVaultUri = new Uri($"https://moneymanagervaultdev.vault.azure.net/");

                    ////configBuilder
                    ////    .AddAzureKeyVault(keyVaultUri, clientId, clientSecret);
                    //configBuilder.AddAzureKeyVault(keyVaultUri,);





                    //var vaultUri = new Uri("https://moneymanagervault2dev.vault.azure.net/");
                    //var vaultUri = new Uri("https://vault.azure.net/.default");
                    //var vaultUri = new Uri("https://moneymanagervaultdev.vault.azure.net/");
                    //var vaultUri = new Uri("https://moneymanagervault2dev.vault.azure.net/.default");


                    //var azureServiceTokenProvider = new AzureServiceTokenProvider("RunAs=App;AppId=d61b6013-0d9b-498b-b668-2b2d76cfdea0");


                    //var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));



                    string secret = "";
                    try
                    {
                        var vaultUri = new Uri("https://moneymanagervaultdev.vault.azure.net/");
                        SecretClient secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());

                        //conStrBuilder.Password = Configuration["AzureSQLPassword"];
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

                        //conStrBuilder.Password = Configuration["AzureSQLPassword"];
                        secret = secretClient2.GetSecret("AzureSQLPasswordDev").Value.Value;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine($"secret2: {secret}");



                    //conStrBuilder.Password = secret;
                    Console.WriteLine($"secret: {secret}");

                    var connection = conStrBuilder.ConnectionString;
                    services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));

                }
                else
                {
                    Console.WriteLine("Use In Memory DB");
                    services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
                }
               
            }
            else
            {
                Console.WriteLine(">>>>>>>>>>>>>> Production Mode");
                if (useAzure)
                {
                    Console.WriteLine("Use Azure 2");

                    var conStrBuilder = new SqlConnectionStringBuilder(
                        //"Server=tcp:moneymanagerdev.database.windows.net,1433;Initial Catalog=moneymanagerdev;Persist Security Info=False;User ID=moneymanagerdevadmin;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                        Configuration.GetConnectionString("SqlServer")
                        );

                    string secret = "";
                    string secret2 = "";
                    //try
                    //{
                    //    var vaultUri = new Uri("https://moneymanagervaultdev.vault.azure.net/");
                    //    SecretClient secretClient = new SecretClient(vaultUri, new DefaultAzureCredential());

                    //    //conStrBuilder.Password = Configuration["AzureSQLPassword"];
                    //    secret = secretClient.GetSecretAsync("AzureSQLPasswordDev").Result.Value.Value;
                    //}catch (Exception ex)
                    //{
                    //    Console.WriteLine("excption for https://moneymanagervaultdev.vault.azure.net/");
                    //    Console.WriteLine(ex.Message);
                    //}

                    //Console.WriteLine($" ============= secret: {secret}");


                    try
                    {
                        var vaultUri2 = new Uri("https://moneymanagervault2dev.vault.azure.net/");


                        //var azureServiceTokenprovider = new AzureServiceTokenProvider();
                        //var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenprovider.KeyVaultTokenCallback));
                        //secret2 = (kv.GetSecretAsync("https://moneymanagervault2dev.vault.azure.net/", "AzureSQLPassword")).WaitAsync().Value.Value;


                        SecretClient secretClient2 = new SecretClient(vaultUri2, new DefaultAzureCredential());
                        secret2 = secretClient2.GetSecretAsync("AzureSQLPassword").Result.Value.Value;

                        //conStrBuilder.Password = Configuration["AzureSQLPassword"];

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("excption for https://moneymanagervault2dev.vault.azure.net/");
                        Console.WriteLine(ex.Message); 
                    }
                    Console.WriteLine($"---------------------- secret2: {secret2}");

                    //if (secret.Length != 0)
                    //{
                    //    conStrBuilder.Password = secret;
                    //}
                    //else if (secret2.Length != 0)
                    //{
                        conStrBuilder.Password = secret2;
                //    }
                //    else
                //    {
                //}


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

            services.AddMediatR(typeof(Startup));
            services.AddControllers();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CategoryService", Version = "v1" });
            });

            services.AddCors(options => options.AddDefaultPolicy(
              //name: "MyPolicy",
              builder =>
              {
                  //builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private async Task<string> GetAccessTokenAsync(string authority, string resource, string scope)
        {
            string ApplicationId = "d61b6013-0d9b-498b-b668-2b2d76cfdea0";
            string ApplicationSecret = "3e633b07-fa51-46ea-a208-907ff214e7ea";

            var appCredentials = new ClientCredential(ApplicationId, ApplicationSecret);
            var context = new AuthenticationContext(authority, TokenCache.DefaultShared);

            var result = await context.AcquireTokenAsync(resource, appCredentials);

            return result.AccessToken;
        }
    }
}
