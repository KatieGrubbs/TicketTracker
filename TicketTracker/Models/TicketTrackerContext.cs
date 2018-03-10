using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class TicketTrackerContext : DbContext
    {
        public TicketTrackerContext() : base("TicketTrackerConnection")
        {
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}