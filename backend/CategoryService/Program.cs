using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace CategoryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
<<<<<<< HEAD:backend/CategoryService/Program.cs
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
=======
          Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
>>>>>>> master:backend/MoneyManagerBackend/TransactionService/Program.cs
    }
}
