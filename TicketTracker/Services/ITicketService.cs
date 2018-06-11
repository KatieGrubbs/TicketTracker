using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TicketTracker.Models;
using TicketTracker.ViewModels;

namespace TicketTracker.Services
{
    public interface ITicketService
    {
        List<TicketViewModel> GetOpenTickets();

        List<TicketViewModel> GetOpenTicketsByUserId(string id);

        List<TicketViewModel> GetResolvedTickets();

        List<TicketViewModel> GetResolvedTicketsByUserId(string id);

        Ticket GetTicketById(int id);

        TicketViewModel GetTicketViewModelById(int id);

        void CreateTicket(Ticket ticket);

        void EditTicket(Ticket ticket);

        void DeleteTicket(int id);

        IEnumerable<SelectListItem> GetSeverityLevels();

        IEnumerable<SelectListItem> GetCategories();
    }
}