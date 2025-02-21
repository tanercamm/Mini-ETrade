namespace ETrade.Domain.Repositories.User
{
    public interface IUserReadRepository
    {
        IQueryable<Entities.User> GetAll();
        Task<Entities.User?> GetByIdAsync(string id);
    }
}
