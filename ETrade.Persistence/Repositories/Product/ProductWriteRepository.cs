using ETrade.Domain.Repositories.Product;
using ETrade.Persistence.Contexts;

namespace ETrade.Infrastructure.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
