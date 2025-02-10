using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Customer
{
    public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer>
    {
        public CustomerReadRepository(DbContext context) : base(context)
        {
        }
    }
}
