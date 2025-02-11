using ETrade.Domain.Entities.Common;
using ETrade.Domain.Repositories;
using ETrade.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETrade.Infrastructure.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly ETradeDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public WriteRepository(ETradeDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task Update(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
