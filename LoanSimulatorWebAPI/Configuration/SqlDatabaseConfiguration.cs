using Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class SqlDatabaseConfiguration
    {
        public static IServiceCollection AddDatabases(this IServiceCollection services, IConfiguration configuration)
        {
            var hackDbConnString = configuration.GetConnectionString("HackathonSqlDb");
            var simulationsDbConnString = configuration.GetConnectionString("SimulationsSqlDb");
            if (string.IsNullOrEmpty(hackDbConnString))
                throw new InvalidOperationException("A string de conexão 'HackathonSqlDb' não foi encontrada.");
            if (string.IsNullOrEmpty(simulationsDbConnString))
                throw new InvalidOperationException("A string de conexão 'SimulationsSqlDb' não foi encontrada.");

            services.AddDbContext<HackDbContext>(options =>
                options.UseSqlServer(hackDbConnString));

            services.AddDbContext<SimulationsDbContext>(options =>
                options.UseSqlServer(simulationsDbConnString));

            return services;
        }
    }
}
