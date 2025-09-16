using Domain.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LoanSimulatorWebAPI.HealthChecks
{
    public class SimulationsDbHealthCheck(ILoanSimulationRepository repository) : IHealthCheck
    {
        private readonly ILoanSimulationRepository _repository = repository;
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {    
            try
            {
                var products = await _repository.GetByDateAsync(DateOnly.FromDateTime(DateTime.Today));
                return HealthCheckResult.Healthy("Database is accessible.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database is not accessible.", ex);
            }
        }
    }
}