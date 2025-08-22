using Infrastructure.Logging;
using Infrastructure.Middlewares;
using Infrastructure.Serialization;
using LoanSimulatorWebAPI.Configuration;
using LoanSimulatorWebAPI.HealthChecks;
using LoanSimulatorWebAPI.Middlewares;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Text.Json;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Host.UseSerilog();

    Log.Information("Iniciando a aplicação...");

    builder.Services.AddControllers(options =>
        {
            options.Filters.Add<LogActionFilter>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.Converters.Add(new DecimalTwoPlacesConverter());
        });
    builder.Services.AddScoped<LogActionFilter>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddCustomSwagger();
    builder.Services.AddDatabases(configuration);
    builder.Services.AddRepositories();
    builder.Services.AddApplication();
    builder.Services.AddValidators();
    builder.Services.AddEventHubProducer(configuration);
    builder.Services.AddOpenTelemetryProvider();
    builder.Services.AddOpenTelemetryExtensions();
    builder.Services.AddAuthConfig(configuration);
    builder.Services.AddHealthChecks()
        .AddCheck<ProductDbHealthCheck>("DatabaseProduto")
        .AddCheck<SimulationsDbHealthCheck>("DatabaseSimulacoes")
        .AddAzureServiceBusTopic(
            connectionString: configuration["EventHub:ConnectionString"],
            topicName: configuration["EventHub:TopicName"],
            name: "eventhub",
            failureStatus: Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy
        );

    if (!builder.Environment.IsDevelopment())
    {
        builder.WebHost.UseUrls("http://+:80");
    }
    var app = builder.Build();

    app.UseSwagger();
    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
        
    }
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LoanSimulatorWebAPI v1");
    });
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.UseMiddleware<TelemetryMiddleware>();
    app.MapControllers();
    app.MapHealthChecks("/health", new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var result = JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(e => new {
                    name = e.Key,
                    status = e.Value.Status.ToString(),
                                duration = e.Value.Duration.ToString()
                })
            });
            await context.Response.WriteAsync(result);
        }
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro fatal na inicializa��o da aplica��o");
}
finally
{
    Log.CloseAndFlush();
}