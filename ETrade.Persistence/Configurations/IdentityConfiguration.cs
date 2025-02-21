using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace ETrade.Persistence.Configurations
{
    public static class IdentityConfiguration
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var roles = new[] { "Admin", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    var result = await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                    if (!result.Succeeded)
                    {
                        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                        throw new Exception($"Role not created: {errors}");
                    }
                }
            }
        }
    }
}
