using Microsoft.EntityFrameworkCore;
using Vehiculos.Models.Vehicle;
using Vehiculos.Models.Client;
using Vehiculos.Models.Owner;
using Vehiculos.Models.Purchase;
using Vehiculos.Models.Sale;
using Vehiculos.Models.Supplier;

namespace Vehiculos.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vehicles> Vehicles { get; set; }
        public DbSet<Propietarios> Propietarios { get; set; }
        public DbSet<Clients> Clients { get; set; }
        public DbSet<Purchases> Purchases { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchases>()
                .Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sales>()
                .Property(s => s.SalePrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Vehiculos.Models.Status.Statuses> Status { get; set; } = default!;

    }
}
