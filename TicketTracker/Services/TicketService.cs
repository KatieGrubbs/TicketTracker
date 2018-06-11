using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using TicketTracker.Models;
using TicketTracker.Models.Utilities;
using TicketTracker.Repositories;
using TicketTracker.ViewModels;

namespace TicketTracker.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public List<TicketViewModel> GetOpenTickets()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Database.SqlQuery<TicketViewModel>(StoredProcedures.GetOpenTickets).ToList();
            }
        }

        public List<TicketViewModel> GetOpenTicketsByUserId(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Database.SqlQuery<TicketViewModel>(StoredProcedures.GetOpenTickets)
                    .Where(t => t.ReporterId == id)
                    .ToList();
            }
        }

        public List<TicketViewModel> GetResolvedTickets()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Database.SqlQuery<TicketViewModel>(StoredProcedures.GetResolvedTickets).ToList();
            }
        }

        public List<TicketViewModel> GetResolvedTicketsByUserId(string id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Database.SqlQuery<TicketViewModel>(StoredProcedures.GetResolvedTickets)
                    .Where(t => t.ReporterId == id)
                    .ToList();
            }
        }

        public Ticket GetTicketById(int id)
        {
            return _ticketRepository.GetById(id);
        }

        public TicketViewModel GetTicketViewModelById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Database.SqlQuery<TicketViewModel>(StoredProcedures.GetTicketById, new SqlParameter("Id", id))
                    .SingleOrDefault();
            }
        }

        public void CreateTicket(Ticket ticket)
        {
            _ticketRepository.Add(ticket);
        }

        public void EditTicket(Ticket ticket)
        {
            _ticketRepository.Update(ticket);
        }

        public void DeleteTicket(int id)
        {
            _ticketRepository.Delete(id);
        }

        public IEnumerable<SelectListItem> GetSeverityLevels()
        {
            using (var db = new ApplicationDbContext())
            {
                var severityLevels = db.SeverityLevels.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Code + " - " + s.Description
                }).ToList();

                return new SelectList(severityLevels, "Value", "Text");
            }
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            using (var db = new ApplicationDbContext())
            {
                var categories = db.Categories.OrderBy(c => c.Name).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();

                return new SelectList(categories, "Value", "Text");
            }
        }
    }
}