using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TicketTracker.Models;

namespace TicketTracker.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public IEnumerable<Ticket> GetAll()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Tickets.ToList();
            }
        }

        public Ticket GetById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Tickets.Find(id);
            }
        }

        public void Add(Ticket ticket)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
            }
        }

        public void Update(Ticket ticket)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var ticket = db.Tickets.Find(id);
                db.Tickets.Remove(ticket);
                db.SaveChanges();
            }
        }
    }
}