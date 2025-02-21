using ETrade.Domain.Repositories.User;

namespace ETrade.Persistence.Repositories.User
{
    public class UserReadRepository : IUserReadRepository
    {
        public IQueryable<Domain.Entities.User> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.User?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
