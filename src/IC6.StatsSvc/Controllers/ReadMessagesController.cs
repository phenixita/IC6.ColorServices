using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus.Core;

namespace IC6.StatsSvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadMessagesController : ControllerBase
    {
        private readonly IMessageReceiver _messageReceiver;

        public ReadMessagesController(IMessageReceiver messageReceiver)
        {
            _messageReceiver = messageReceiver;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<ICollection<string>>> Read()
        {
            var receivedMessages = 0;
            var messagesToBeReturned = new List<string>();


            while (true)
            {

                Trace.WriteLine("\nReceiving messages.");

                var receivedMessage = await _messageReceiver.ReceiveAsync(TimeSpan.FromSeconds(3));
                if (receivedMessage != null)
                {

                    var message = Encoding.UTF8.GetString(receivedMessage.Body);
                    Trace.WriteLine(message);
                    messagesToBeReturned.Add(message);

                    //Trace.WriteLine("CorrelationId={0}", receivedMessage.CorrelationId);
                    receivedMessages++;

                    // Complete the message so that it is not received again.
                    await _messageReceiver.CompleteAsync(receivedMessage.SystemProperties.LockToken);

                }
                else
                {
                    // No more messages to receive.
                    break;
                }
            }

            return messagesToBeReturned;
        }

    }
}
