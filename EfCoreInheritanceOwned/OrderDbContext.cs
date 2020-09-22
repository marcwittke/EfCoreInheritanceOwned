using Microsoft.EntityFrameworkCore;
using EfCoreInheritanceOwned.Orders;

namespace EfCoreInheritanceOwned
{
    public class OrderDbContext : DbContext
    {
        public DbSet<ExternalOrder> ExternalOrders { get; set; }
        public DbSet<InternalOrder> InternalOrders { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,14330;Database=TestDb33;User=sa;Password=Metropolitan2019");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("purchasing");

            modelBuilder.Entity<Order>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property(o => o.Id).ValueGeneratedNever();
                entityTypeBuilder.Property<byte[]>("Timestamp").IsRowVersion();
                entityTypeBuilder.Property(o => o.Recipient).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<InternalOrder>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<byte[]>("Timestamp").IsRowVersion();
                entityTypeBuilder.OwnsOne(io => io.Store,
                    builder => builder.Property<byte[]>("Timestamp").IsRowVersion());
            });

            modelBuilder.Entity<ExternalOrder>(entityTypeBuilder =>
            {
                entityTypeBuilder.Property<byte[]>("Timestamp").IsRowVersion();
                entityTypeBuilder.OwnsOne(eo => eo.Supplier,
                    builder => builder.Property<byte[]>("Timestamp").IsRowVersion());
            });
        }
    }
}