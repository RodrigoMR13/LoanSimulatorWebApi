using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ILoanSimulationRepository
    {
        Task<int> GetTotalRecords();
        Task<IEnumerable<LoanSimulation>> GetAllAsync(PaginationRequest paginationRequest);
        Task<LoanSimulation?> GetByIdAsync(int id);
        Task<IEnumerable<LoanSimulation>> GetByDateAsync(DateOnly date);
        Task<LoanSimulation> AddAsync(LoanSimulation loanSimulation);
    }
}