using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ticket_ManagementAPI.Model
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(250)]
        public string? Address { get; set; }

        [MaxLength(15)]
        public string? MobileNumber { get; set; }

        [MaxLength(100)]
        public string? Designation { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
    }
}
