﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IC6.Weather
{
    public class Program
    {
        public static string WeatherDataLayerUrl = Environment.GetEnvironmentVariable("WEATHER_DATALAYER_SERVICE_URL") ?? "localhost";
        public static string WeatherDataLayerPort = Environment.GetEnvironmentVariable("WEATHER_DATALAYER_SERVICE_PORT") ?? "53671";


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
