using LoanSimulatorWebAPI.HealthChecks;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck<ProductDbHealthCheck>("DatabaseProduto")
                .AddCheck<SimulationsDbHealthCheck>("DatabaseSimulacoes")
                .AddAzureServiceBusTopic(
                    connectionString: configuration["EventHub:ConnectionString"],
                    topicName: configuration["EventHub:TopicName"],
                    name: "eventhub",
                    failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy
                );

            return services;
        }
    }
}
