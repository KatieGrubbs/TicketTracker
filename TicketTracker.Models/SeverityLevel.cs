using System.ComponentModel.DataAnnotations;

namespace TicketTracker.Models
{
    public class SeverityLevel
    {
        [Key]
        public int SeverityLevelId { get; set; }

        [Required]
        public string SeverityLevelCode { get; set; }

        [Required]
        public string SeverityLevelName { get; set; }
    }
}