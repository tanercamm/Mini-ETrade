using ETrade.Domain.Repositories.Order;
using ETrade.Persistence.Contexts;

namespace ETrade.Infrastructure.Repositories.Order
{
    public class OrderReadRepository : ReadRepository<Domain.Entities.Order>, IOrderReadRepository
    {
        public OrderReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
