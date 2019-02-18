using System;

namespace TicketManagement.Data.Exceptions
{
    public class PageNotFoundException : Exception
    {
        public PageNotFoundException(string message = "Page with this number not found") : base(message)
        {
        }
    }
}