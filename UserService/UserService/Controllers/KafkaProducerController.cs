using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Controllers {
    
    [Route("api/kafka")]
    [ApiController]
    public class KafkaProducerController : ControllerBase {


        private readonly ProducerConfig config = new ProducerConfig
        { BootstrapServers = "localhost:9092" };
        private readonly string topic = "AdminTopic";
        [HttpPost]
        public IActionResult Post([FromQuery] string message) {
            //return Ok("test");
            return Created(string.Empty, SendToKafka(topic, message));
        }
        private Object SendToKafka(string topic, string message) {
            using (var producer =
                 new ProducerBuilder<Null, string>(config).Build()) {
                try {
                    return producer.ProduceAsync(topic, new Message<Null, string> { Value = message })
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e) {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
            return null;
        }
    }
}