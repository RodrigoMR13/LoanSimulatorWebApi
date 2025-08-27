using Domain.Interfaces;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace LoanSimulatorWebAPI.HealthChecks
{
    public class ProductDbHealthCheck(IProductRepository productRepository) : IHealthCheck
    {
        private readonly IProductRepository _productRepository = productRepository;
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {    
            try
            {
                var products = await _productRepository.GetAllAsync();
                return HealthCheckResult.Healthy("Database is accessible.");
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy("Database is not accessible.", ex);
            }
        }
    }
}