using Microsoft.AspNetCore.Identity;

namespace ETrade.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
    }
}
