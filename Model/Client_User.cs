using Duende.IdentityModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_ManagementAPI.Model
{
    public class Client_User
    {

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Cli_Id { get; set; }

            [Required(ErrorMessage = "Client name is required")]
            [MaxLength(100, ErrorMessage = "Client name cannot exceed 100 characters")]
            public string Cli_Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Mobile number is required")]
            [Phone(ErrorMessage = "Invalid mobile number format")]
            [MaxLength(15, ErrorMessage = "Mobile number cannot exceed 15 digits")]
            public string Cli_MobileNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email ID is required")]
            [EmailAddress(ErrorMessage = "Invalid email format")]
            [MaxLength(100, ErrorMessage = "Email ID cannot exceed 100 characters")]
            public string Cli_EmailId { get; set; } = string.Empty;

            [Required(ErrorMessage = "Address is required")]
            [MaxLength(250, ErrorMessage = "Address cannot exceed 250 characters")]
            public string Cli_Address { get; set; } = string.Empty;

            [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
            public string? Cli_Description { get; set; }

            // Foreign key
            [Required(ErrorMessage = "Entity Type is required")]
            [ForeignKey("Entity")]
            public int EntityType { get; set; }
            public virtual Entity Entity { get; set; }
        }

}
