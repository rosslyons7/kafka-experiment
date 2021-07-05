using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminService.Services {
    public class AdminTopicManagement : IAdminTopicManagement {

        private readonly ProducerConfig config = new ProducerConfig
        { BootstrapServers = "localhost:9092" };
        private readonly string topic = "AdminTopic";

        public async Task<object> SendToKafka(string message) {
             using (var producer =
                 new ProducerBuilder<Null, string>(config).Build()) {
               try {
                     return  producer.ProduceAsync(topic, new Message<Null, string> { Value = message })
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
