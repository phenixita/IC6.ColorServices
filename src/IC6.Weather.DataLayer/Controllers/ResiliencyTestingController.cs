using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IC6.Weather.DataLayer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResiliencyTestingController : ControllerBase
    {

        [HttpGet("AddDelaySeconds")]
        public string AddDelaySeconds()
        {
            ResiliencyTesting.SecondsAddedOfDelay++;

            return $"Seconds of delay: {ResiliencyTesting.SecondsAddedOfDelay}.";
        }

        [HttpGet("ResetDelaySeconds")]
        public string ResetDelaySeconds()
        {
            ResiliencyTesting.SecondsAddedOfDelay = 0;

            return $"Seconds of delay: {ResiliencyTesting.SecondsAddedOfDelay}.";
        }

        [HttpGet("SetServiceDown")]
        public string SetServiceDown()
        {
            ResiliencyTesting.ServiceDown = true;

            return $"Service down: {ResiliencyTesting.ServiceDown}.";
        }

        [HttpGet("SetServiceAvailable")]
        public string SetServiceAvailable()
        {
            ResiliencyTesting.ServiceDown = false;

            return $"Service down: {ResiliencyTesting.ServiceDown}.";
        }
    }
}