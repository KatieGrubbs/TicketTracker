using System;
using System.Collections.Generic;
using System.Text;

namespace TicketTracker.Models.Utilities
{
    public class StoredProcedures
    {
        public const string GetOpenTickets = "GetOpenTickets";
        public const string GetResolvedTickets = "GetResolvedTickets";
        public const string GetTicketById = "GetTicketById @Id";
    }
}
