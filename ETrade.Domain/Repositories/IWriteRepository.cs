using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        // CRUD işlemlerini burada tanımlanmalı for SOLID

        Task AddAsync(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task SaveChangesAsync();
    }
}
