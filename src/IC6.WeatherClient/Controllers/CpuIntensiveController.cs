using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace IC6.WeatherClient.Controllers
{
    public class CpuIntensiveController : Controller
    {
        public IActionResult Index()
        { 
             Enumerable
                  .Range(1, Environment.ProcessorCount) // replace with lesser number if 100% usage is not what you are after.
                  .AsParallel()
                  .Select(i =>
                  {
                      var end = DateTime.Now + TimeSpan.FromSeconds(10);
                      while (DateTime.Now < end)
                          /*nothing here */
                          ;
                      return i;
                  })
                  .ToList(); // ToList makes the query execute.

            ViewBag.CpuIntensive = "Completed CPU intensive task.";

            return View();
        }
    }
}