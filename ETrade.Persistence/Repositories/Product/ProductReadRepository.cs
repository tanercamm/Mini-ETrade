using ETrade.Domain.Repositories.Product;
using ETrade.Persistence.Contexts;

namespace ETrade.Infrastructure.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<Domain.Entities.Product>, IProductReadRepository
    {
        public ProductReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
