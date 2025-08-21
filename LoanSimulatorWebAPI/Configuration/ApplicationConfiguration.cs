using Application;
using Application.Services;
using Application.UseCases;
using Application.Validators;
using FluentValidation;
using MediatR;

namespace LoanSimulatorWebAPI.Configuration
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ILoanSimulatorService, LoanSimulatorService>();
            services.AddTransient<ISacCalculatorUseCase, SacCalculatorUseCase>();
            services.AddTransient<IPriceCalculatorUseCase, PriceCalculatorUseCase>();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly);
            });

            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<SimulateLoanRequestValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
