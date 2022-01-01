using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CategoryService.Domains.Model;
using CategoryService.Domains.Repository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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

               if (_env.IsDevelopment())
            {
                Console.WriteLine("Development Mode");
                Console.WriteLine("Use In Memory DB");
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
            }
            else
            {
            Console.WriteLine("Production Mode");

            var conStrBuilder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("SqlServer"));
            conStrBuilder.Password = Configuration["AzureSQLPassword"];
            var connection = conStrBuilder.ConnectionString; 
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection));
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
                  builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
              }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CategoryService v1"));
            }

            app.UseHttpsRedirection();

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
