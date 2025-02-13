using ETrade.Domain.Entities;
using ETrade.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Persistence.Contexts
{
    public class ETradeDbContext : DbContext
    {
        public ETradeDbContext(DbContextOptions<ETradeDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer silinirse, ona ait Order'lar da silinecek
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Products)
                .WithOne(c => c.Order)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Product>()
                .HasMany(p => p.Orders)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });
        }
    }
}
