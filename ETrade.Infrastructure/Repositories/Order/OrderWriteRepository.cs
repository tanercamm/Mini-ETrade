using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Order
{
    public class OrderWriteRepository : WriteRepository<Domain.Entities.Order>
    {
        public OrderWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
