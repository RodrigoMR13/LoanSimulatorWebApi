using Application.Dtos.Requests;
using Application.Dtos;
using Application.UseCases;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services
{
    public class LoanSimulatorService(ISacCalculatorUseCase sacCalculator, IPriceCalculatorUseCase priceCalculator) : ILoanSimulatorService
    {
        private readonly ISacCalculatorUseCase _sacCalculator = sacCalculator;
        private readonly IPriceCalculatorUseCase _priceCalculator = priceCalculator;

        public SimulationDto SimulateSac(SimulateLoanRequest request, decimal interestRate)
        {
            var installments = _sacCalculator.Calculate(request.Value, request.Period, interestRate);

            var result = new SimulationDto()
            {
                SimulationType = AmortizationMethodsEnum.SAC,
                Installments = installments
            };

            return result;
        }

        public SimulationDto SimulatePrice(SimulateLoanRequest request, decimal interestRate)
        {
            var installments = _priceCalculator.Calculate(request.Value, request.Period, interestRate);

            var result = new SimulationDto()
            {
                SimulationType = AmortizationMethodsEnum.PRICE,
                Installments = installments
            };

            return result;
        }
    }
}
