using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class Ticket
    {
        public Ticket()
        {
            DateCreated = DateTime.Now;
        }

        [Key]
        public int TicketId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int SeverityLevelId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        public string ReporterId { get; set; }

        [Required]
        public bool IsResolved { get; set; }
    }
}