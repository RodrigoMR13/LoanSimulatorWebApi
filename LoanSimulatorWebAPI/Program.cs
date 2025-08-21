using LoanSimulatorWebAPI.Configuration;
using LoanSimulatorWebAPI.Middlewares;
using Serilog;
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

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddCustomSwagger();
    builder.Services.AddDatabases(configuration);
    builder.Services.AddRepositories();
    builder.Services.AddApplication();
    builder.Services.AddValidators();

    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseAuthorization();
    app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro fatal na inicialização da aplicação");
}
finally
{
    Log.CloseAndFlush();
}