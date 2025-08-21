using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HackDbContext _dbContext;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(HackDbContext dbContext, ILogger<ProductRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            //try
            //{
            _logger.LogInformation("Obtendo todos os produtos no Banco de Dados...");
            var dbResponse = await _dbContext.Produtos.AsNoTracking().ToListAsync();
            _logger.LogInformation($"Produtos obtidos. Quantidade: {dbResponse.Count()}");
            return dbResponse;
            //}
            //catch (DbException ex)
            //{
            //    _logger.LogError($"Erro ao buscar produtos no Banco de Dados.\n Exception: {ex.Message}");
            //    return null;
            //}
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            //try
            //{
            _logger.LogInformation($"Obtendo produto de id {id} no Banco de Dados...");
            var dbResponse = await _dbContext.Produtos.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);
            _logger.LogInformation($"Produto de id {id} obtido.");
            return dbResponse;
            //}
            //catch (ProductNotFoundException ex)
            //{
            //    _logger.LogError(ex.Message);
            //    throw;
            //}
            //catch (DbException ex)
            //{
            //    _logger.LogError($"Erro ao buscar produto de id {id} no Banco de Dados.\n Exception: {ex.Message}");
            //    return null;
            //}
        }
    }
}
