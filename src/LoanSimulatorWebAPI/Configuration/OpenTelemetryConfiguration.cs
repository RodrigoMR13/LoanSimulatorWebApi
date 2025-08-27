using Application.OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class OpenTelemetryConfiguration
    {
        public static IServiceCollection AddOpenTelemetryProvider(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService("LoanSimulatorWebAPI"))
                .WithTracing(tracing =>
                {
                    tracing
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation();

                    tracing.AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri("http://jaeger:4317");
                    });
                });

            return services;
        }

        public static IServiceCollection AddOpenTelemetryExtensions(this IServiceCollection services)
        {
            services.AddSingleton<TelemetryStorage>();
            return services;
        }
    }
}
