using Application.Interfaces;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Infrastructure.Services
{
    public class EventHubProducerService(EventHubProducerClient producerClient, ILogger<EventHubProducerService> logger) : IEventHubProducer
    {
        private readonly EventHubProducerClient _producerClient = producerClient;
        private readonly ILogger<EventHubProducerService> _logger = logger;

        public async Task SendMessageAsync(string message)
        {
            _logger.LogInformation("Enviando mensagem da simulação para o EventHub...");
            byte[] bytes = Encoding.UTF8.GetBytes(message);

            using var eventBatch = await _producerClient.CreateBatchAsync();
            if (!eventBatch.TryAdd(new EventData(bytes)))
            {
                throw new Exception("The message is too large to fit in the batch.");
            }

            await _producerClient.SendAsync(eventBatch);
        }
    }
}
