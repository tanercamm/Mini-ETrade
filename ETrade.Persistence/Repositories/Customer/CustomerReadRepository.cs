using ETrade.Domain.Repositories.Customer;
using ETrade.Persistence.Contexts;

namespace ETrade.Infrastructure.Repositories.Customer
{
    public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>, ICustomerReadRepository
    {
        public CustomerReadRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
