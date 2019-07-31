using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.Azure.ServiceBus;


namespace IC6.RedSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandomObjectController : ControllerBase
    {
        private readonly IQueueClient _queueClient;

        public RandomObjectController(IQueueClient queueClient)
        {
            _queueClient = queueClient;
        }

        readonly string[] objects = { "Sea", "Bugatti", "Sky", "Marker", "Whale" };

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            Trace.TraceInformation("User requested a random green object.");

            // Create a new message to send to the queue
            string messageBody = objects[DateTime.Now.Ticks % objects.Length];
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));
            await _queueClient.SendAsync(message);

            return messageBody;
        }

        //fault inhection

        //resiliency

        //retry pattern

        //logging vs tracing in a container

        //transaction id
    }
}
