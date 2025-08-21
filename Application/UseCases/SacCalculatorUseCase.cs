using Application.Dtos;

namespace Application.UseCases
{
    public class SacCalculatorUseCase : ISacCalculatorUseCase
    {
        public SacCalculatorUseCase() { }

        public List<InstallmentsDto> Calculate(decimal value, int period, decimal interestRate)
        {
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
