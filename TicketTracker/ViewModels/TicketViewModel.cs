using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketTracker.ViewModels
{
    public class TicketViewModel
    {
        [Key]
        [Display(Name = "Ticket ID")]
        public int TicketId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Severity Level")]
        public int SeverityLevelId { get; set; }
        public string SeverityLevelName { get; set; }
        public IEnumerable<SelectListItem> SeverityLevels { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Required]
        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        [Required]
        [Display(Name = "Reporter")]
        public string ReporterId { get; set; }
        public string ReporterName { get; set; }

        [Required]
        [Display(Name = "Resolved?")]
        public bool IsResolved { get; set; }
    }
}