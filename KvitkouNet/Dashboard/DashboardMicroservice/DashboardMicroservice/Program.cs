using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using DashboardMicroService;

namespace Dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(b => b.AddJsonFile("appsettings.json",
                    true,
                    true))
                .ConfigureAppConfiguration(builder => builder.AddEnvironmentVariables())
                .UseStartup<Startup>();
        }
    }
}
