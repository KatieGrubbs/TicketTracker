using System.Collections.Generic;
using TicketTracker.Models;

namespace TicketTracker.Repositories
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();

        Ticket GetById(int id);

        void Add(Ticket ticket);

        void Update(Ticket ticket);

        void Delete(int id);
    }
}
