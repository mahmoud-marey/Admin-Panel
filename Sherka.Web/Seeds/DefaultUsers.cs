using Microsoft.AspNetCore.Identity;

namespace Sherka.Web.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser admin = new()
            {
                UserName = "admin",
                FullName = "Admin",
                Email = "admin@admin.com",
                EmailConfirmed = true                
            };
            var user = await userManager.FindByEmailAsync(admin.Email);
            if(user is null)
            {
                await userManager.CreateAsync(admin,"@Admin_Password123");
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }
        }

    }
}
