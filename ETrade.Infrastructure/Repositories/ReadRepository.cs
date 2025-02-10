using ETrade.Domain.Entities.Common;
using ETrade.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public ReadRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task<T> GetByIdAsync(string id)
        {
            if(!Guid.TryParse(id, out Guid guid))
                return null;
            
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == guid);
        }
    }
}
