using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>
    {
        public ProductWriteRepository(DbContext context) : base(context)
        {
        }
    }
}
