using ETrade.Domain.Repositories.Customer;
using ETrade.Persistence.Contexts;

namespace ETrade.Infrastructure.Repositories.Customer
{
    public class CustomerWriteRepository : WriteRepository<Domain.Entities.Customer>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(ETradeDbContext context) : base(context)
        {
        }
    }
}
