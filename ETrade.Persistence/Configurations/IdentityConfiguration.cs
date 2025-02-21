using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using ETrade.Domain.Entities;

namespace ETrade.Persistence.Configurations
{
    public static class IdentityConfiguration
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

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

            var adminEmail = "admin@gmail.com";
            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin == null)
            {
                var adminUser = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };
                var createResult = await userManager.CreateAsync(adminUser, "admin123");
                if (createResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("✅ Varsayılan admin oluşturuldu.");
                }
                else
                {
                    var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                    Console.WriteLine($"❌ Admin oluşturulamadı: {errors}");
                }
            }
            else
            {
                Console.WriteLine("ℹ️ Varsayılan admin zaten mevcut.");
            }
        }
    }
}
