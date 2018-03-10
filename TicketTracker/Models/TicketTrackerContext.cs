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
        
        public System.Data.Entity.DbSet<TicketTracker.Models.Category> Categories { get; set; }
        public System.Data.Entity.DbSet<TicketTracker.Models.Priority> Priorities { get; set; }
        public System.Data.Entity.DbSet<TicketTracker.Models.Ticket> Tickets { get; set; }
    }
}