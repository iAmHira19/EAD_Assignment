using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ExamManagementSystem.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "SuperAdmin", "Admin", "Clerk" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Optionally Create a Default SuperAdmin User
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string adminEmail = "superadmin@example.com";
            string adminPassword = "Super@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(adminUser, adminPassword);
                await userManager.AddToRoleAsync(adminUser, "SuperAdmin");
            }
        }
    }
}
