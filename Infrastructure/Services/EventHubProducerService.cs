using Application.Dtos.Responses;
using Application.Interfaces;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class EventHubProducerService(EventHubProducerClient producerClient) : IEventHubProducer
    {
        private readonly EventHubProducerClient _producerClient = producerClient;

        public async Task SendMessageAsync(string message)
        {
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
