using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IC6.RedSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomObjectController : ControllerBase
    {
        string[] redObjects = { "Ferrari", "Coca Cola", "Pendolino", "Cherry", "Strawberry" };

        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return redObjects[DateTime.Now.Ticks % redObjects.Length];
        }

        //fault inhection

        //resiliency

        //retry pattern
    }
}
