using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.SqlServer.Context
{
    public class SimulationsDbContextFactory : IDesignTimeDbContextFactory<SimulationsDbContext>
    {
        public SimulationsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SimulationsDbContext>();
            optionsBuilder.UseSqlServer("Server=localhost,1434;Database=SeuBanco;User Id=sa;Password=SuaSenha;TrustServerCertificate=True;");

            return new SimulationsDbContext(optionsBuilder.Options);
        }
    }
}
