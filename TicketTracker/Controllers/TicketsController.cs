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
using TicketTracker.Repositories;
using TicketTracker.Services;
using TicketTracker.ViewModels;

namespace TicketTracker.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController()
        {
            _ticketService = new TicketService(new TicketRepository());
        }

        // GET: Tickets/OpenTickets
        [HttpGet]
        public ActionResult OpenTickets()
        {
            var openTickets = new List<TicketViewModel>();

            if (User.IsInRole(UserRoles.Administrator))
            {
                openTickets = _ticketService.GetOpenTickets();
            }
            else
            {
                openTickets = _ticketService.GetOpenTicketsByUserId(User.Identity.GetUserId());
            }

            return View(openTickets);
        }

        // GET: Tickets/ResolvedTickets
        [HttpGet]
        public ActionResult ResolvedTickets()
        {
            var resolvedTickets = new List<TicketViewModel>();

            if (User.IsInRole(UserRoles.Administrator))
            {
                resolvedTickets = _ticketService.GetResolvedTickets();
            }
            else
            {
                resolvedTickets = _ticketService.GetResolvedTicketsByUserId(User.Identity.GetUserId());
            }

            return View(resolvedTickets);
        }

        // GET: Tickets/Details/5
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ticketViewModel = _ticketService.GetTicketViewModelById((int)id);

            if (ticketViewModel == null)
            {
                return HttpNotFound();
            }

            return View(ticketViewModel);
        }

        // GET: Tickets/Create
        [HttpGet]
        public ActionResult Create()
        {
            // Make sure that dropdownlists in the create form are populated
            var ticketViewModel = new TicketViewModel
            {
                SeverityLevels = _ticketService.GetSeverityLevelsDropdown(),
                Categories = _ticketService.GetCategoriesDropdown()
            };

            return View(ticketViewModel);
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketViewModel ticketViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ticketViewModel);
            }

            // Create new ticket from viewmodel property values
            var ticket = new Ticket
            {
                Id = ticketViewModel.Id,
                Subject = ticketViewModel.Subject,
                Description = ticketViewModel.Description,
                SeverityLevelId = ticketViewModel.SeverityLevelId,
                CategoryId = ticketViewModel.CategoryId,
                ReporterId = User.Identity.GetUserId(),     // Get logged in user
                IsResolved = ticketViewModel.IsResolved,
                IsDeleted = ticketViewModel.IsDeleted
            };

            _ticketService.CreateTicket(ticket);

            return RedirectToAction("OpenTickets", "Tickets");
        }

        // GET: Tickets/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ticketViewModel = _ticketService.GetTicketViewModelById((int)id);

            if (ticketViewModel == null)
            {
                return HttpNotFound();
            }

            // Make sure that dropdownlists in the edit form are populated
            ticketViewModel.SeverityLevels = _ticketService.GetSeverityLevelsDropdown();
            ticketViewModel.Categories = _ticketService.GetCategoriesDropdown();

            return View(ticketViewModel);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TicketViewModel ticketViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ticketViewModel);
            }

            // Create new ticket from viewmodel property values
            var ticket = new Ticket
            {
                Id = ticketViewModel.Id,
                Subject = ticketViewModel.Subject,
                Description = ticketViewModel.Description,
                SeverityLevelId = ticketViewModel.SeverityLevelId,
                CategoryId = ticketViewModel.CategoryId,
                DateCreated = ticketViewModel.DateCreated,
                ReporterId = ticketViewModel.ReporterId,
                IsResolved = ticketViewModel.IsResolved,
                IsDeleted = ticketViewModel.IsDeleted
            };

            _ticketService.EditTicket(ticket);

            return RedirectToAction("OpenTickets", "Tickets");
        }

        [Authorize(Roles = UserRoles.Administrator)]
        public ActionResult FlagAsResolved(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new ApplicationDbContext())
            {
                var ticketViewModel = _ticketService.GetTicketViewModelById((int)id);

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                // Mark ticket as resolved
                ticketViewModel.IsResolved = true;

                var ticket = new Ticket
                {
                    Id = ticketViewModel.Id,
                    Subject = ticketViewModel.Subject,
                    Description = ticketViewModel.Description,
                    SeverityLevelId = ticketViewModel.SeverityLevelId,
                    CategoryId = ticketViewModel.CategoryId,
                    DateCreated = ticketViewModel.DateCreated,
                    ReporterId = ticketViewModel.ReporterId,
                    IsResolved = ticketViewModel.IsResolved,
                    IsDeleted = ticketViewModel.IsDeleted
                };

                _ticketService.EditTicket(ticket);

                return RedirectToAction("OpenTickets", "Tickets");
            }
        }

        [Authorize(Roles = UserRoles.Administrator)]
        public ActionResult FlagAsOpen(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ticketViewModel = _ticketService.GetTicketViewModelById((int)id);

            if (ticketViewModel == null)
            {
                return HttpNotFound();
            }

            // Re-open ticket
            ticketViewModel.IsResolved = false;

            var ticket = new Ticket
            {
                Id = ticketViewModel.Id,
                Subject = ticketViewModel.Subject,
                Description = ticketViewModel.Description,
                SeverityLevelId = ticketViewModel.SeverityLevelId,
                CategoryId = ticketViewModel.CategoryId,
                DateCreated = ticketViewModel.DateCreated,
                ReporterId = ticketViewModel.ReporterId,
                IsResolved = ticketViewModel.IsResolved,
                IsDeleted = ticketViewModel.IsDeleted
            };

            _ticketService.EditTicket(ticket);

            return RedirectToAction("ResolvedTickets", "Tickets");
        }

        [Authorize(Roles = UserRoles.Administrator)]
        public ActionResult FlagAsDeleted(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var ticketViewModel = _ticketService.GetTicketViewModelById((int)id);

            if (ticketViewModel == null)
            {
                return HttpNotFound();
            }

            // Soft-delete ticket
            ticketViewModel.IsDeleted = true;

            var ticket = new Ticket
            {
                Id = ticketViewModel.Id,
                Subject = ticketViewModel.Subject,
                Description = ticketViewModel.Description,
                SeverityLevelId = ticketViewModel.SeverityLevelId,
                CategoryId = ticketViewModel.CategoryId,
                DateCreated = ticketViewModel.DateCreated,
                ReporterId = ticketViewModel.ReporterId,
                IsResolved = ticketViewModel.IsResolved,
                IsDeleted = ticketViewModel.IsDeleted
            };

            _ticketService.EditTicket(ticket);

            return RedirectToAction("OpenTickets", "Tickets");
        }
    }
}
