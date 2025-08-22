using Application.OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics.Metrics;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class OpenTelemetryConfiguration
    {
        public static IServiceCollection AddOpenTelemetryProvider(this IServiceCollection services)
        {
            services.AddOpenTelemetry().WithTracing(builder => 
            {
                builder
                    .AddSource("LoanSimulatorWebAPI")
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("LoanSimulatorWebAPI"))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter();
            }).WithMetrics(metricsBuilder =>
            {
                metricsBuilder
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("LoanSimulatorWebAPI"))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddConsoleExporter();
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
