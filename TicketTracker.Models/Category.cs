using System.ComponentModel.DataAnnotations;

namespace TicketTracker.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}