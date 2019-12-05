using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IC6.Weather.DataLayer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherDataController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherDataController> _logger;

        public WeatherDataController(ILogger<WeatherDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            if (ResiliencyTesting.ServiceDown)
            {
                throw new OperationCanceledException();
            }

            if (ResiliencyTesting.SecondsAddedOfDelay > 0)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(ResiliencyTesting.SecondsAddedOfDelay));
            }

            var jitterer = new Random(DateTime.Now.Second);
            System.Threading.Thread.Sleep(jitterer.Next(500,2000));

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
