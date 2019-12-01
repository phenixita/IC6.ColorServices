using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IC6.WeatherClient.Models;
using RestSharp;
using Polly;

namespace IC6.WeatherClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var client = new RestClient($"http://{Program.WeatherServiceUrl}:{Program.WeatherServicePort}");
            var request = new RestRequest("/WeatherForecast/");

            //We assume that by default the weather service is not available.
            //So we provide a default value like N.A.
            ViewBag.WeatherForecast = "N.A.";
            ViewBag.ClientRestRequestInfo = $"client.BaseUrl = {client.BaseUrl}, request.Resource = {request.Resource}";

            //We define a timeout policy to provide resiliency and we won't create a indefinite
            //wait for a low priority service.
            var policy = Policy.Timeout(1, onTimeout: (context, timeSpan, task) =>
            {

                _logger.LogWarning($"{context.PolicyKey} at {context.OperationKey}: execution timed out after {timeSpan.TotalSeconds} seconds.");

            }, timeoutStrategy: Polly.Timeout.TimeoutStrategy.Pessimistic); //This is not a best practice for production workloads with Polly. https://github.com/App-vNext/Polly/wiki/Timeout

            //We query the weather service through the defined policy.
            try
            {
                policy.Execute(() =>
                {

                    var weatherForecast = client.Execute(request);

                    ViewBag.WeatherForecast = weatherForecast.Content;
                });
            }
            catch (Polly.Timeout.TimeoutRejectedException) { }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
