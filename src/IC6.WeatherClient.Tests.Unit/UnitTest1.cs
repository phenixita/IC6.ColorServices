using IC6.WeatherClient.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace IC6.WeatherClient.Tests.Unit
{
    [TestClass]
    public class MemoryIntensiveControllerTests
    {
        [TestMethod]
        public void Index()
        {
            
            //Act.
            var cut = new MemoryIntensiveController();
            cut.Index();

            //Assert. 

            //At least 1 GBi.
            Assert.IsTrue(cut.ViewBag.ReservedMemory >= Math.Pow(2.0, 30.0));
        }
    }
}
