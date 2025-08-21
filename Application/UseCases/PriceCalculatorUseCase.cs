using Application.Dtos;

namespace Application.UseCases
{
    public class PriceCalculatorUseCase : IPriceCalculatorUseCase
    {
        public PriceCalculatorUseCase() { }

        public List<InstallmentsDto> Calculate(decimal value, int period, decimal interestRate)
        {
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
