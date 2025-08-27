using Application.Dtos;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    public class PriceCalculatorUseCase(ILogger<PriceCalculatorUseCase> logger) : IPriceCalculatorUseCase
    {
        private readonly ILogger<PriceCalculatorUseCase> _logger = logger;

        public List<InstallmentsDto> Calculate(decimal value, int period, decimal interestRate)
        {
            _logger.LogInformation("Calculando parcelas do Sistema PRICE...");
            var installments = new List<InstallmentsDto>();

            if (interestRate == 0)
            {
                var installmentValue = value / period;
                for (short month = 1; month <= period; month++)
                {
                    installments.Add(new InstallmentsDto(month, installmentValue, 0, installmentValue));
                }
                return installments;
            }

            var factor = (decimal)(Math.Pow(1 + (double)interestRate, period) * (double)interestRate /
                                   (Math.Pow(1 + (double)interestRate, period) - 1));
            var installmentValueFixed = value * factor;
            var remainingBalance = value;

            for (short month = 1; month <= period; month++)
            {
                var interest = remainingBalance * interestRate;
                var amortization = installmentValueFixed - interest;
                var installment = new InstallmentsDto(month, amortization, interest, installmentValueFixed);
                installments.Add(installment);
                remainingBalance -= amortization;
            }

            return installments;
        }
    }
}
