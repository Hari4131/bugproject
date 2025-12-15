using Microsoft.AspNetCore.Identity;
using Ticket_ManagementAPI.Model;

namespace Ticket_ManagementAPI.Data
{
    public static class RoleInitializer
    {
        public static async Task SeedRolesAndUsers(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roleNames = { "Admin", "Ticket-Agent", "Client" };


            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            var adminEmail = "admin@bug_test.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var user = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin#$bug_test@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }


        public static async Task SeedEntities(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await context.Database.EnsureCreatedAsync();

                var entityNames = new[]
                {
            "Retail Service",
            "Financial Services",
            "Healthcare Services",
            "Travel Services",
            "Hardware Services"
        };

                foreach (var name in entityNames)
                {
                    if (!context.Entities.Any(e => e.Ent_Name == name))
                    {
                        context.Entities.Add(new Entity { Ent_Name = name });
                    }
                }

                await context.SaveChangesAsync();
            }
        }

        public static async Task TicketStatus(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                await context.Database.EnsureCreatedAsync();

                var bug_testNames = new[]
                {
            "Open",
            "Inprogress",
            "Closed",
            "Unresolved"
          
        };

                foreach (var name in bug_testNames)
                {
                    if (!context.Ticket_Statuses.Any(e => e.Tic_Sta_Name == name))
                    {
                        context.Ticket_Statuses.Add(new Ticket_Status { Tic_Sta_Name = name });
                    }
                }

                await context.SaveChangesAsync();
            }


        }

    }
}
