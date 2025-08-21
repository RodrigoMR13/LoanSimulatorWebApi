using Application.Dtos.Requests;
using Application.Dtos;

namespace Application.Services
{
    public interface ILoanSimulatorService
    {
        SimulationDto SimulateSac(SimulateLoanRequest request, decimal interestRate);
        SimulationDto SimulatePrice(SimulateLoanRequest request, decimal interestRate);
    }
}
