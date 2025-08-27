using Application.Dtos;

namespace Application.UseCases
{
    public interface IPriceCalculatorUseCase
    {
        /// <summary>
        /// Calcula as parcelas de um empréstimo pelo sistema Price (parcelas fixas).
        /// </summary>
        /// <param name="value">Valor total do empréstimo</param>
        /// <param name="period">Número de meses</param>
        /// <param name="interestRate">Taxa de juros mensal (em decimal, ex: 2% = 0.02)</param>
        /// <returns>Dto que representa a simulação</returns>
        List<InstallmentsDto> Calculate(decimal value, int period, decimal interestRate);
    }
}
