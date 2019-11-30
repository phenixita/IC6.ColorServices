using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IC6.WeatherClient
{
    public class Program
    {
        public static string WeatherServiceUrl = Environment.GetEnvironmentVariable("WEATHER_SERVICE_URL") ?? "localhost";
        public static string WeatherServicePort = Environment.GetEnvironmentVariable("WEATHER_SERVICE_PORT") ?? "32313";

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
