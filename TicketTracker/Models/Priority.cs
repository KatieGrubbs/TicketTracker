using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class Priority
    {
        [Key]
        public int PriorityId { get; set; }

        [Required]
        public string PriorityCode { get; set; }

        [Required]
        public string PriorityName { get; set; }
    }
}