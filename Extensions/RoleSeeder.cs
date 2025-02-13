using UTEvents.Context;
using UTEvents.Entities;

namespace UTEvents.Extensions
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(UTEventsContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "Student" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
