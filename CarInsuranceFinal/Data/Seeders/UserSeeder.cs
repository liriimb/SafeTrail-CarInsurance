using CarInsuranceFinal.Models.Schema;
using Microsoft.AspNetCore.Identity;

namespace CarInsuranceFinal.Data.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedUsers(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null || userManager == null)
            {
                throw new InvalidOperationException("RoleManager or UserManager is not available.");
            }

            // Seed roles
            await RoleSeeder.SeedRoles(roleManager);

            // Seed admin user
            var adminEmail = "admin@admin.com";
            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "Admin",
                    LastName = null,
                    MultiCar = true,
                    EmailConfirmed = true,
                };

                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RoleSeeder.Role_Agent);
                }
            }
            else
            {
                if (!await userManager.IsInRoleAsync(user, RoleSeeder.Role_Agent))
                {
                    await userManager.AddToRoleAsync(user, RoleSeeder.Role_Agent);
                }
            }
        }
    }
}
