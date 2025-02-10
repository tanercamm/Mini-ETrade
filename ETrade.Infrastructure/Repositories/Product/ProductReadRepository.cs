using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<Domain.Entities.Product>
    {
        public ProductReadRepository(DbContext context) : base(context)
        {
        }
    }
}
