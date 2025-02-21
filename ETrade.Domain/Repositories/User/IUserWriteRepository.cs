namespace ETrade.Domain.Repositories.User
{
    public interface IUserWriteRepository
    {
        Task<bool> AddAsync(Entities.User user, string password);
        Task<bool> Update(Entities.User user);
        Task<bool> Delete(string id);
    }
}
