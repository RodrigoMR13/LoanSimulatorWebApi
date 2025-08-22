using Application.Dtos;
using Microsoft.Extensions.Logging;

namespace Application.UseCases
{
    public class SacCalculatorUseCase(ILogger<SacCalculatorUseCase> logger) : ISacCalculatorUseCase
    {
        private readonly ILogger<SacCalculatorUseCase> _logger = logger;

        public List<InstallmentsDto> Calculate(decimal value, int period, decimal interestRate)
        {
            _logger.LogInformation("Calculando parcelas do Sistema SAC...");
            var installments = new List<InstallmentsDto>();
            var amortization = value / period;
            var remainingBalance = value;

            for (short month = 1; month <= period; month++)
            {
                var interest = remainingBalance * interestRate;
                var installmentValue = amortization + interest;
                var installment = new InstallmentsDto(month, amortization, interest, installmentValue);
                installments.Add(installment);
                remainingBalance -= amortization;
            }

            return installments;
        }
    }
}
