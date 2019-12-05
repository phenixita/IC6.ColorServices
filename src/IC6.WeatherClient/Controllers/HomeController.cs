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
using System.Threading;
using Polly.Retry;

namespace IC6.WeatherClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private static int _timeoutSeconds = 2;
        private static int _retries = 3;
        private static bool _expRetries = false;

        [HttpGet("SetPolicies")]
        public string SetPolicies(int timeoutSeconds, int retries, bool expRetries, int weatherServicePort)
        {
            _timeoutSeconds = timeoutSeconds;
            _retries = retries;
            _expRetries = expRetries;
            Program.WeatherServicePort = weatherServicePort.ToString();

            return $"Timeout seconds: {_timeoutSeconds} / Retries {_retries} / Exp. retries {_expRetries}";
        }

        public IActionResult Index()
        {
            var client = new RestClient($"http://{Program.WeatherServiceUrl}:{Program.WeatherServicePort}");
            var request = new RestRequest("/WeatherForecast/");


            ViewBag.WeatherForecast = "N.A.";
            ViewBag.ClientRestRequestInfo = $"client.BaseUrl = {client.BaseUrl}, request.Resource = {request.Resource}";
            ViewBag.ProcessInfo = Environment.MachineName;

            if (ResiliencyTesting.ServiceDown)
            {
                throw new OperationCanceledException();
            }

            if (ResiliencyTesting.SecondsAddedOfDelay > 0)
            {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(ResiliencyTesting.SecondsAddedOfDelay));
            }

            var timeoutPolicy = Policy.Timeout(_timeoutSeconds, Polly.Timeout.TimeoutStrategy.Pessimistic);


            RetryPolicy retryPolicy;
            if (_expRetries)
            {
                retryPolicy = Policy.Handle<Polly.Timeout.TimeoutRejectedException>()
                            .WaitAndRetry(_retries, retryAttempt =>
                                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            }
            else
            {
                retryPolicy = Policy.Handle<Polly.Timeout.TimeoutRejectedException>()
                                                  .WaitAndRetry(_retries, retryAttempt =>
                                    TimeSpan.FromSeconds(3));
            }


            var globalPolicy = Policy.Wrap(retryPolicy, timeoutPolicy);
            var context = new Context();
            context.Add("weatherForecast", "");
            globalPolicy.Execute((c) =>
            {
                context["weatherForecast"] = client.Execute(request).Content;
            }, context);

            ViewBag.WeatherForecast = context["weatherForecast"];
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
