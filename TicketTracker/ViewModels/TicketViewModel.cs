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
        public TicketViewModel()
        {
            SeverityLevels = new List<SelectListItem>();
            Categories = new List<SelectListItem>();
            DateCreated = DateTime.Now;
        }

        [Key]
        [Display(Name = "Ticket ID")]
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Severity is required")]
        [Display(Name = "Severity Level ID")]
        public int SeverityLevelId { get; set; }

        [Display(Name = "Severity")]
        public string SeverityLevelName { get; set; }

        [Display(Name = "Severity")]
        public IEnumerable<SelectListItem> SeverityLevels { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Reporter ID")]
        public string ReporterId { get; set; }

        [Display(Name = "Reporter")]
        public string ReporterName { get; set; }

        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public bool IsResolved { get; set; }

        public string Resolved => IsResolved ? "Resolved" : "Open/Active";
    }
}