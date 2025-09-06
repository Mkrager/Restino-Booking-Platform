using Microsoft.AspNetCore.Identity;
using Restino.Application.Constants;
using Restino.Identity.Models;

namespace Restino.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));

            if (!await roleManager.RoleExistsAsync(Roles.Default))
                await roleManager.CreateAsync(new IdentityRole(Roles.Default));
        }
    }
}
