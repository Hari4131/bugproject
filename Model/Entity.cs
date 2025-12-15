using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket_ManagementAPI.Model
{
    public class Entity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Ent_Id { get; set; }

        [Required(ErrorMessage = "Entity name is required")]
        [MaxLength(100, ErrorMessage = "Entity name cannot exceed 100 characters")]
        public string Ent_Name { get; set; }
    }
}
