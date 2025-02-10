using ETrade.Domain.Entities.Common;

namespace ETrade.Domain.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        // SELECT işlemleri Read içerisinden tanımlanmalı for SOLID

        // Sorgu için IQueryable (where). Eğer Memory ise IEnumable || List mantıklı olacaktır

        IQueryable<T> GetAll();

        Task<T?> GetByIdAsync(string id); // id guid olarak tanımlandığından string ile algılayacağız
    }
}
