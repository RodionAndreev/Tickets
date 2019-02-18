using Dashboard.Logic.Models.Enums;
using System.Collections.Generic;

namespace Dashboard.Logic.Models
{
    public class News
    {
        /// <summary>
        ///     Статус новости
        /// </summary>
        public NewsStatus NewsStatus { get; set; }
    
        /// <summary>
        ///     Цена билета
        /// </summary>
        public virtual TicketInfo Ticket { get; set; }
    }
}
