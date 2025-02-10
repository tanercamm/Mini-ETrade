using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Order
{
    public class OrderReadRepository : ReadRepository<Domain.Entities.Order>
    {
        public OrderReadRepository(DbContext context) : base(context)
        {
        }
    }
}
