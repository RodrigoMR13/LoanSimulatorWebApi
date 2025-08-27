using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SqlServer.Context
{
    public class SimulationsDbContext(DbContextOptions<SimulationsDbContext> options) : DbContext(options)
    {
        public DbSet<LoanSimulation> LoanSimulations { get; set; } = null!;
        public DbSet<Simulation> Simulations { get; set; } = null!;
        public DbSet<Installment> Installments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoanSimulation>()
                .HasMany(ls => ls.Simulations)
                .WithOne(s => s.LoanSimulation)
                .HasForeignKey(s => s.LoanSimulationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Simulation>()
                .HasMany(s => s.Installments)
                .WithOne(i => i.Simulation)
                .HasForeignKey(i => i.SimulationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Installment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.AmortizationValue).HasColumnType("numeric(18,2)");
                entity.Property(e => e.InterestValue).HasColumnType("numeric(18,2)");
                entity.Property(e => e.InstallmentValue).HasColumnType("numeric(18,2)");
                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("DATEADD(HOUR, -3, GETUTCDATE())")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<LoanSimulation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.InterestRate).HasColumnType("numeric(10,9)");
                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("DATEADD(HOUR, -3, GETUTCDATE())")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Simulation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedDateTime)
                    .HasDefaultValueSql("DATEADD(HOUR, -3, GETUTCDATE())")
                    .ValueGeneratedOnAdd();
            });

        }
    }
}
