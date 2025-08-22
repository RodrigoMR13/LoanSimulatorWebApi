using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
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

        public async Task<int> GetTotalRecordsAsync()
        {
            _logger.LogInformation("Obtendo o número total de simulações no Banco de Dados...");
            var query = _dbContext.LoanSimulations.AsQueryable();
            int totalRecords = await query.CountAsync();
            _logger.LogInformation($"Query executada com sucesso. Total: {totalRecords}");
            return totalRecords;
        }

        public async Task<int> GetTotalRecordsByDateAsync(DateOnly date)
        {
            _logger.LogInformation($"Buscando o número de simulações para a data {date} no Banco de Dados...");
            var query = _dbContext.LoanSimulations
                .Where(ls => DateOnly.FromDateTime(ls.CreatedDateTime) == date)
                .AsQueryable();
            var totalRecords = await query.CountAsync();
            _logger.LogInformation($"Query executada com sucesso! Total: {totalRecords}.");
            return totalRecords;
        }

        public async Task<IEnumerable<LoanSimulation>> GetAllAsync(PaginationRequest paginationRequest)
        {
            _logger.LogInformation($"Buscando simulações no Banco de Dados com paginação. Página:{paginationRequest.PageNumber}, Tamanho: {paginationRequest.RecordsQtyPerPage}...");
            var query = _dbContext.LoanSimulations
                .Include(ls => ls.Simulations)
                .ThenInclude(s => s.Installments)
                .AsQueryable();

            var records = await query
                .Skip((paginationRequest.PageNumber - 1) * paginationRequest.RecordsQtyPerPage)
                .Take(paginationRequest.RecordsQtyPerPage)
                .ToListAsync();

            _logger.LogInformation($"Simulações obtidas com sucesso! Número de simulações: {records.Count}");
            return records;
        }

        public async Task<LoanSimulation?> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Buscando simulação de ID {id} no Banco de Dados...");
            var dbResponse = await _dbContext.LoanSimulations.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            _logger.LogInformation($"Simulação de ID {id} obtida com sucesso!");
            return dbResponse;
        }
        
        public async Task<IEnumerable<LoanSimulation>> GetByDateAsync(DateOnly date)
        {
            _logger.LogInformation($"Buscando simulações pela data {date} no Banco de Dados...");
            var query = _dbContext.LoanSimulations
                .Include(ls => ls.Simulations)
                .ThenInclude(s => s.Installments)
                .Where(ls => DateOnly.FromDateTime(ls.CreatedDateTime) == date)
                .AsQueryable();

            var records = await query.ToListAsync();
            _logger.LogInformation($"Query executada com sucesso! Número de simulações: {records.Count}.");
            return records;
        }

        public async Task<LoanSimulation> AddAsync(LoanSimulation loanSimulation)
        {
            _logger.LogInformation($"Adicionando simulação de empréstimo {JsonSerializer.Serialize(loanSimulation)} no Banco de Dados...");
            await _dbContext.LoanSimulations.AddAsync(loanSimulation);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Simulação de empréstimo adicionada no Banco de Dados com sucesso! Id: {loanSimulation.Id}.");
            return loanSimulation;
        }
    }
}
