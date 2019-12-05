﻿using System;
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

        private static int _timeoutSeconds = 2;
        private static int _retries = 2;
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

            //We assume that by default the wea_ther service is not available.
            //So we provide a default value like N.A.
            ViewBag.WeatherForecast = "N.A.";
            ViewBag.ClientRestRequestInfo = $"client.BaseUrl = {client.BaseUrl}, request.Resource = {request.Resource}";
            ViewBag.ProcessInfo = Environment.MachineName;




            var weatherForecast = client.Execute(request);

            ViewBag.WeatherForecast = weatherForecast.Content;


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
