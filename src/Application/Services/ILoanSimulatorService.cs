using Application.Dtos.Requests;
using Application.Dtos;

namespace Application.Services
{
    public interface ILoanSimulatorService
    {
        SimulationDto SimulateSac(CreateLoanSimulationRequest request, decimal interestRate);
        SimulationDto SimulatePrice(CreateLoanSimulationRequest request, decimal interestRate);
    }
}
