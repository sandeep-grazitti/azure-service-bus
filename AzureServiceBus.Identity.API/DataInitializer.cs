using AzureServiceBus.Identity.API.Model;
using Microsoft.AspNetCore.Identity;

namespace AzureServiceBus.Identity.API
{
    public class DataInitializer
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        private static void SeedUsers(UserManager<ApplicationUser> userManager)
        { 
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    LockoutEnabled = false,
                    TwoFactorEnabled = false,
                    FirstName = "Admin",
                    LastName = "User"
                };
                IdentityResult result = userManager
                    .CreateAsync(user, "P@ssw0rd1!").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }

        private static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                ApplicationRole role = new ApplicationRole
                {
                    Name = "Admin",
                };
                _ = roleManager.CreateAsync(role).Result.Succeeded;
            }
        }
    }
}
