using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Restino.Identity.Models;
using Restino.Identity.Seeds;

namespace Restino.Identity.Initialization
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task SeedIdentityAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await DefaultRoles.SeedAsync(userManager, roleManager);
            await DefaultSuperAdmin.SeedAsync(userManager, roleManager);
            await DefaultBasicUser.SeedAsync(userManager, roleManager);
        }
    }
}
