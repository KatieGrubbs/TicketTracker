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

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Severity Level is required")]
        [Display(Name = "Severity Level ID")]
        public int SeverityLevelId { get; set; }
        [Display(Name = "Severity Level")]
        public string SeverityLevelName { get; set; }
        public IEnumerable<SelectListItem> SeverityLevels { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Reporter ID")]
        public string ReporterId { get; set; }
        [Display(Name = "Reporter")]
        public string ReporterName { get; set; }

        [Required(ErrorMessage = "Resolved? is required")]
        [Display(Name = "Resolved?")]
        public bool IsResolved { get; set; }

        public string Resolved => IsResolved ? "Yes" : "No";
    }
}