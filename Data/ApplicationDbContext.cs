using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ticket_ManagementAPI.Model;

namespace Ticket_ManagementAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client_User> Client_Users { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Ticket_Status> Ticket_Statuses { get; set; }


    }
}
