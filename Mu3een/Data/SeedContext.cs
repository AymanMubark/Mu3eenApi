using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mu3een.Entities;

namespace Mu3een.Data
{
    public static class SeedContext
    {
        public static async Task initalApp(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<Mu3eenContext>();

            await context!.Database.MigrateAsync();

            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            if (!await roleManager.Roles.AnyAsync())
            {
                string[] roles = new string[]
                {
                    "Admin",
                    "Volunteer",
                    "Institution",
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
                }
                if (!await context.Users.AnyAsync())
                {
                    var admin = new Admin { UserName = "Admin", Email = "admin@admin.com" };
                    await userManager.CreateAsync(admin, "P@$$0rd");
                    await userManager.AddToRoleAsync(admin, nameof(Admin));
                }
            }
          
            if (!await context.SocialEventTypes.AnyAsync())
            {
                await context.SocialEventTypes.AddRangeAsync(new SocialEventType[]
                   {
                    new SocialEventType
                    {
                        Name = "Mediacl",
                        NameAr = "طبي",
                    } , new SocialEventType
                    {
                        Name = "Religon",
                        NameAr = "ديني",
                    }
                   });
                await context.SaveChangesAsync();
            }

        }
    }
}
