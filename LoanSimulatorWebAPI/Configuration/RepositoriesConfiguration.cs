using Domain.Interfaces;
using Infrastructure.Repositories;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
