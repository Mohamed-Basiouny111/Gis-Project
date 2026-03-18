using Gis_Project.Context;
using Gis_Project.Models;
using Microsoft.AspNetCore.Identity;

namespace Gis_Project.SeedData
{
    public static class SeedData
    {
        public static async Task SeedAppAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, GisContext context)
        {
            if (!roleManager.Roles.Any())
            {
                var roles = new List<string> { "Admin", "Collector", "NewUser" };
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            if (!userManager.Users.Any())
            {
                var admin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    FUllName = "Admin",
                    UserName = "admin",
                    PhoneNumber = "1234567890",    
                    IsBlocked = false,      
                };
                await userManager.CreateAsync(admin, "P@ssW0rd");
                await userManager.AddToRoleAsync(admin, "Admin");

                var student = new ApplicationUser
                {
                    Email = "user1@gmail.com",
                    FUllName = "user1",
                    UserName = "user1",
                    PhoneNumber = "1234567890",               
                   IsBlocked = false,
                };
                await userManager.CreateAsync(student, "P@ssW0rd");
                await userManager.AddToRoleAsync(student, "Collector");

                var service = new ApplicationUser
                {
                    Email = "user2@gmail.com",
                    FUllName = "user2",
                    UserName = "user2",
                    PhoneNumber = "1234567890",                
                    IsBlocked = false,
                };
                await userManager.CreateAsync(service, "P@ssW0rd");
                await userManager.AddToRoleAsync(service, "NewUser");

            }

        }
    }
}
