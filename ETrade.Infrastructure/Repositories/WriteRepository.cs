using ETrade.Domain.Entities.Common;
using ETrade.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public WriteRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(T entity)
        {
             _dbSet.Remove(entity);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
