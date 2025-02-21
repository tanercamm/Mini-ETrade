using ETrade.Domain.Entities;
using ETrade.Domain.Repositories.User;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Persistence.Repositories.User
{
    public class UserWriteRepository : IUserWriteRepository
    {
        private readonly UserManager<Domain.Entities.User> _userManager;

        public UserWriteRepository(UserManager<Domain.Entities.User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AddAsync(Domain.Entities.User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded) return false;

            // Varsayılan olarak "Customer" rolü ata
            await _userManager.AddToRoleAsync(user, "Customer");
            return true;
        }

        public async Task<bool> Update(Domain.Entities.User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            else
            {
                return false;
            }
        }
    }
}
