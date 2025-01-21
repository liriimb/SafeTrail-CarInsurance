using Microsoft.AspNetCore.Identity;

namespace CarInsuranceFinal.Data.Seeders
{
    public static class RoleSeeder
    {
        public const string Role_Agent = "Agent";
        public const string Role_Client = "Client";

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            // Check if the "Agent" role exists, if not, create it
            if (await roleManager.FindByNameAsync(Role_Agent) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Role_Agent));
            }

            // Check if the "Client" role exists, if not, create it
            if (await roleManager.FindByNameAsync(Role_Client) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Role_Client));
            }
        }
    }
}
