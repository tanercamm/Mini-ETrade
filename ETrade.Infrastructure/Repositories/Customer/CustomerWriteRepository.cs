using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Customer
{
    public class CustomerWriteRepository : WriteRepository<Domain.Entities.Customer>
    {
        public CustomerWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
