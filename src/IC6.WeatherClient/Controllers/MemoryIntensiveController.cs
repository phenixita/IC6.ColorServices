using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IC6.WeatherClient.Controllers
{
    public class MemoryIntensiveController : Controller
    {
        public IActionResult Index()
        {
            var oneGBi = Math.Pow(2.0, 30.0);
            var bytes = new byte[(int)oneGBi];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(i % 256);
            }
            using (var mem = new MemoryStream(bytes, true))
            {
                ViewBag.ReservedMemory = mem.Capacity;

                System.Threading.Thread.Sleep(3);


            }

            return View();
        }

    }
}