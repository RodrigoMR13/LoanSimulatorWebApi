using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer.Context
{
    public class HackDbContext(DbContextOptions<HackDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Produtos { get; set; }
    }
}
