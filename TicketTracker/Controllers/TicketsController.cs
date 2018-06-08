using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketTracker.Models;
using TicketTracker.Models.Utilities;
using TicketTracker.ViewModels;

namespace TicketTracker.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        // GET: Tickets/OpenTickets
        public ActionResult OpenTickets()
        {
            var openTickets = new List<TicketViewModel>();

            using (var db = new ApplicationDbContext())
            {
                if (User.IsInRole(UserRoles.Administrator))
                {
                    // call stored procedure and return a list of all open tickets
                    openTickets = db.Database.SqlQuery<TicketViewModel>("GetOpenTickets").ToList();
                }
                else
                {
                    // call stored procedure and return a list of open tickets matching the user's id
                    openTickets = db.Database.SqlQuery<TicketViewModel>("GetOpenTickets")
                        .Where(t => t.ReporterId == User.Identity.GetUserId()).ToList();
                }

                return View(openTickets);
            }
        }

        // GET: Tickets/ResolvedTickets
        public ActionResult ResolvedTickets()
        {
            var resolvedTickets = new List<TicketViewModel>();

            using (var db = new ApplicationDbContext())
            {
                if (User.IsInRole("Admin"))
                {
                    // call stored procedure and return a list of all resolved tickets
                    resolvedTickets = db.Database.SqlQuery<TicketViewModel>("GetResolvedTickets").ToList();
                }
                else
                {
                    // call stored procedure and return a list of resolved tickets matching the user's id
                    resolvedTickets = db.Database.SqlQuery<TicketViewModel>("GetResolvedTickets")
                        .Where(t => t.ReporterId == User.Identity.GetUserId()).ToList();
                }

                return View(resolvedTickets);
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId",
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                return View(ticketViewModel);
            }
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            // make sure that dropdownlists in the create form are populated
            var ticketViewModel = new TicketViewModel
            {
                SeverityLevels = GetSeverityLevels(),
                Categories = GetCategories()
            };

            return View(ticketViewModel);
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    // create new ticket from viewmodel property values
                    var ticket = new Ticket
                    {
                        TicketId = ticketViewModel.TicketId,
                        Subject = ticketViewModel.Subject,
                        Description = ticketViewModel.Description,
                        SeverityLevelId = ticketViewModel.SeverityLevelId,
                        CategoryId = ticketViewModel.CategoryId,
                        ReporterId = User.Identity.GetUserId(),     // get logged in user
                        IsResolved = ticketViewModel.IsResolved,
                        IsDeleted = ticketViewModel.IsDeleted
                    };

                    db.Tickets.Add(ticket);
                    db.SaveChanges();

                    return RedirectToAction("OpenTickets", "Tickets");
                }
            }

            return View();
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId", 
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                // make sure that dropdownlists in the edit form are populated
                ticketViewModel.SeverityLevels = GetSeverityLevels();
                ticketViewModel.Categories = GetCategories();

                return View(ticketViewModel);
            }
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    // create new ticket from viewmodel property values
                    var ticket = new Ticket
                    {
                        TicketId = ticketViewModel.TicketId,
                        Subject = ticketViewModel.Subject,
                        Description = ticketViewModel.Description,
                        SeverityLevelId = ticketViewModel.SeverityLevelId,
                        CategoryId = ticketViewModel.CategoryId,
                        DateCreated = ticketViewModel.DateCreated,
                        ReporterId = ticketViewModel.ReporterId,
                        IsResolved = ticketViewModel.IsResolved,
                        IsDeleted = ticketViewModel.IsDeleted
                    };

                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("OpenTickets", "Tickets");
                }
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FlagAsResolved(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId",
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                // mark ticket as resolved
                ticketViewModel.IsResolved = true;

                var ticket = new Ticket
                {
                    TicketId = ticketViewModel.TicketId,
                    Subject = ticketViewModel.Subject,
                    Description = ticketViewModel.Description,
                    SeverityLevelId = ticketViewModel.SeverityLevelId,
                    CategoryId = ticketViewModel.CategoryId,
                    DateCreated = ticketViewModel.DateCreated,
                    ReporterId = ticketViewModel.ReporterId,
                    IsResolved = ticketViewModel.IsResolved,
                    IsDeleted = ticketViewModel.IsDeleted
                };

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("OpenTickets", "Tickets");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FlagAsOpen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId",
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                // re-open ticket
                ticketViewModel.IsResolved = false;

                var ticket = new Ticket
                {
                    TicketId = ticketViewModel.TicketId,
                    Subject = ticketViewModel.Subject,
                    Description = ticketViewModel.Description,
                    SeverityLevelId = ticketViewModel.SeverityLevelId,
                    CategoryId = ticketViewModel.CategoryId,
                    DateCreated = ticketViewModel.DateCreated,
                    ReporterId = ticketViewModel.ReporterId,
                    IsResolved = ticketViewModel.IsResolved,
                    IsDeleted = ticketViewModel.IsDeleted
                };

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("ResolvedTickets", "Tickets");
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult FlagAsDeleted(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId",
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                // soft-delete ticket
                ticketViewModel.IsDeleted = true;

                var ticket = new Ticket
                {
                    TicketId = ticketViewModel.TicketId,
                    Subject = ticketViewModel.Subject,
                    Description = ticketViewModel.Description,
                    SeverityLevelId = ticketViewModel.SeverityLevelId,
                    CategoryId = ticketViewModel.CategoryId,
                    DateCreated = ticketViewModel.DateCreated,
                    ReporterId = ticketViewModel.ReporterId,
                    IsResolved = ticketViewModel.IsResolved,
                    IsDeleted = ticketViewModel.IsDeleted
                };

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("OpenTickets", "Tickets");
            }
        }
        
        private IEnumerable<SelectListItem> GetSeverityLevels()
        {
            using (var db = new ApplicationDbContext())
            {
                var severityLevels = db.SeverityLevels.Select(s => new SelectListItem
                {
                    Value = s.SeverityLevelId.ToString(),
                    Text = s.SeverityLevelCode + " - " + s.SeverityLevelName
                }).ToList();

                return new SelectList(severityLevels, "Value", "Text");
            }
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories.OrderBy(c => c.CategoryName).Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

                return new SelectList(categories, "Value", "Text");
            }
        }
    }
}
