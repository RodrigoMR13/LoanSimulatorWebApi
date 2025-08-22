using System.Security.Cryptography.X509Certificates;
using Domain.Common;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class LoanSimulationRepository(SimulationsDbContext dbContext, ILogger<LoanSimulationRepository> logger) : ILoanSimulationRepository
    {
        private readonly SimulationsDbContext _dbContext = dbContext;
        private readonly ILogger<LoanSimulationRepository> _logger = logger;

        public async Task<int> GetTotalRecords()
        {
            var query = _dbContext.LoanSimulations.AsQueryable();
            int totalRecords = await query.CountAsync();
            return totalRecords;
        }

        public async Task<IEnumerable<LoanSimulation>> GetAllAsync(PaginationRequest paginationRequest)
        {
            var query = _dbContext.LoanSimulations
                .Include(ls => ls.Simulations)
                .ThenInclude(s => s.Installments)
                .AsQueryable();

            var records = await query
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.RecordsQtyPerPage)
                .Take(paginationRequest.RecordsQtyPerPage)
                .ToListAsync();

            _logger.LogInformation($"Simulações obtidas. Quantidade: {records.Count}");
            return records;
        }

        public async Task<LoanSimulation?> GetByIdAsync(int id)
        {
            var dbResponse = await _dbContext.LoanSimulations.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            return dbResponse;
        }
        
        public async Task<IEnumerable<LoanSimulation>> GetByDateAsync(DateOnly date)
        {
            var query = _dbContext.LoanSimulations
                .Include(ls => ls.Simulations)
                .ThenInclude(s => s.Installments)
                .Where(ls => DateOnly.FromDateTime(ls.CreatedDateTime) == date)
                .AsQueryable();

            var records = await query.ToListAsync();
            
            _logger.LogInformation($"Simulações obtidas. Quantidade: {records.Count}");
            return records;
        }

        public async Task<LoanSimulation> AddAsync(LoanSimulation loanSimulation)
        {
            await _dbContext.LoanSimulations.AddAsync(loanSimulation);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Simulação de empréstimo adicionada com ID: {loanSimulation.Id}");
            return loanSimulation;
        }
    }
}
