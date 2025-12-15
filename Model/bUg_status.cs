using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_ManagementAPI.Model
{
    public class Ticket_Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Tic_Sta_Id { get; set; }

        [Required(ErrorMessage ="Ticket Status need to be present")]
        [MaxLength(15, ErrorMessage = "Entity name cannot exceed 15 characters")]
        public string Tic_Sta_Name { get; set; }
    }
}
