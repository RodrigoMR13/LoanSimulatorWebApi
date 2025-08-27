using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class ProductRepository(HackDbContext dbContext, ILogger<ProductRepository> logger) : IProductRepository
    {
        private readonly HackDbContext _dbContext = dbContext;
        private readonly ILogger<ProductRepository> _logger = logger;

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            _logger.LogInformation("Obtendo todos os produtos no Banco de Dados...");
            var dbResponse = await _dbContext.Produtos.AsNoTracking().ToListAsync();
            _logger.LogInformation($"Produtos obtidos com sucesso! Quantidade: {dbResponse.Count()}");
            return dbResponse;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            _logger.LogInformation($"Buscando produto de ID {id} no Banco de Dados...");
            var dbResponse = await _dbContext.Produtos.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            _logger.LogInformation($"produto de ID {id} obtido com sucesso!");
            return dbResponse;
        }
    }
}
