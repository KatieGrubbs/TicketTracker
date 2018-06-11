using System.ComponentModel.DataAnnotations;

namespace TicketTracker.Models
{
    public class SeverityLevel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }
    }
}