using Application.Interfaces;
using Azure.Messaging.EventHubs.Producer;
using Infrastructure.Services;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class EventHubConfiguration
    {
        public static IServiceCollection AddEventHubProducer(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(sp =>
            {
                var connectionString = config["EventHub:ConnectionString"];
                var hubName = config["EventHub:HubName"];
                return new EventHubProducerClient(connectionString, hubName);
            });
            services.AddScoped<IEventHubProducer, EventHubProducerService>();
            return services;
        }
    }
}
