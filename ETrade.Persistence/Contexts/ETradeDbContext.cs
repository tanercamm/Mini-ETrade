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

    }
}
