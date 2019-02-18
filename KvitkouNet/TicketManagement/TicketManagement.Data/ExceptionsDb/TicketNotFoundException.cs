using System;

namespace TicketManagement.Data.Exceptions
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string message = "Ticket not found") : base(message)
        {
        }
    }
}