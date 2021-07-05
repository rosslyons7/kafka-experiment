using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kafka.Public;
using Microsoft.Extensions.Logging;
using Kafka.Public.Loggers;
using System.Text;

namespace AdminService.Handlers {
    public class AdminTopicConsumerHandler : IHostedService {

        private readonly string topic = "AdminTopic";
        private readonly ILogger<AdminTopicConsumerHandler> _logger;
        private ClusterClient _cluster;

        public AdminTopicConsumerHandler(ILogger<AdminTopicConsumerHandler> logger) {
            _logger = logger;
            _cluster = new ClusterClient(new Configuration()
            {
                Seeds = "localhost:9092"
            }, new ConsoleLogger());
        }

        public Task StartAsync(CancellationToken cancellationToken) {

            _cluster.ConsumeFromEarliest(topic);
            _cluster.MessageReceived += record =>
            {
                var message = Encoding.UTF8.GetString(record.Value as byte[]);
                Console.WriteLine($"Received: {message}");
                _logger.LogInformation($"Received: {message}");
            };
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken) {
            _cluster?.Dispose();
            return Task.CompletedTask;
        }

    }
}
