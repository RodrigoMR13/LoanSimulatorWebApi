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
            var dbResponse = await _dbContext.Produtos.AsNoTracking().ToListAsync();
            _logger.LogInformation($"Produtos obtidos. Quantidade: {dbResponse.Count()}");
            return dbResponse;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var dbResponse = await _dbContext.Produtos.AsNoTracking().SingleOrDefaultAsync(p => p.Id == id);   
            return dbResponse;
        }
    }
}
