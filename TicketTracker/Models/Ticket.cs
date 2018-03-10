using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketTracker.ViewModels
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }

        [Required]
        public int PriorityId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public bool IsResolved { get; set; }
    }
}