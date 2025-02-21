namespace ETrade.Application.DTOs.User
{
    public class AssignRoleDTO
    {
        public Guid UserId { get; set; }

        public string Role { get; set; } = string.Empty;
    }
}
