using ETrade.Application.DTOs.User;

namespace ETrade.Application.Services.Abstract
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDTO registerDTO);

        Task<string?> LoginAsync(LoginDTO loginDTO);

        Task<UserDTO?> GetUserByIdAsync(Guid id);
    }
}
