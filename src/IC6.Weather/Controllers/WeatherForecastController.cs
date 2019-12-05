using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace IC6.Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var client = new RestClient($"http://{Program.WeatherDataLayerUrl}:{Program.WeatherDataLayerPort}");
            var request = new RestRequest("/WeatherData/");

            if (ResiliencyTesting.ServiceDown)
            {
                throw new OperationCanceledException();
            }

            if (ResiliencyTesting.SecondsAddedOfDelay > 0)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(ResiliencyTesting.SecondsAddedOfDelay));
            }

            var weatherDataLayerResult = client.Execute(request);

            return weatherDataLayerResult.Content;


        }
    }
}
