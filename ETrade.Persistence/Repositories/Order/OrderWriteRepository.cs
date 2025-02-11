using ETrade.Domain.Repositories.Order;
using ETrade.Persistence.Contexts;

namespace ETrade.Infrastructure.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<Domain.Entities.Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
