using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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