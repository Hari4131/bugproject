using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_ManagementAPI.Model
{
    public class Tickets
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Tic_Id { get; set; }

        [Required(ErrorMessage = "Reason for the bug_test")]
        [MaxLength(100, ErrorMessage = "Client name cannot exceed 100 characters")]
        public string Tic_Name { get; set; }

        public int Tic_UniqueNum { get; set; }

        [Required(ErrorMessage = "Reason for the bug_test")]
        [MaxLength(100, ErrorMessage = "Client name cannot exceed 100 characters")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Reason for the bug_test")]
        [MaxLength(100, ErrorMessage = "Client name cannot exceed 100 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ticket Status is Required")]
        [ForeignKey("Ticket_Status")]
        public int Tic_Status { get; set; }

        public virtual Ticket_Status Ticket_Status { get; set; }

        // Foreign key
        [Required(ErrorMessage = "Client Id is required")]
        [ForeignKey("Client_User")]
        public int Tic_ClientId { get; set; }
        public virtual Client_User Client_User { get; set; }

    }


    public class GetAllTickets
    {

        public string Cli_MobileNumber { get; set; }
        public string Cli_EmailId { get; set; }
        public string Cli_Description { get; set; }
        public string Cli_Name { get; set; }
        public string Cli_Address { get; set; }
        public int Cli_Id { get; set; }
        public int Tic_UniqueNum { get; set; }
        public string Tic_Name { get; set; }
        public string Subject { get; set; }
        public int Tic_StatusId { get; set; }
        public string Description { get; set; }

        public string Tic_StatusDes { get; set; }

    }


    public class TicketLoginDetails
    {
       public string Cli_EmailId { get; set; }
        public string Cli_MobileNumber { get; set; }
        public string Cli_Name { get; set; }

        public string Cli_EntityDetails { get; set; }
        public string Role { get; set; }

        public GetAllTickets getAllTickets { get; set; }

    }

   


   


    }
