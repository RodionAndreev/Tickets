using System;
using Dashboard.Data.DbModels.DbEnums;

namespace Dashboard.Data.DbModels
{
    public class NewsDb
    {
        /// <summary>
        ///     Id билета
        /// </summary>
        public string NewsId { get; set; }

        /// <summary>
        ///     Id билета
        /// </summary>
        public NewsStatusDb NewsStatus { get; set; }

        /// <summary>
        ///     Дата создания билета
        /// </summary>
        public DateTime CreatedDate { get; set; }

        #region Связи между таблицами 
        /// <summary>
        ///     Билет на мероприятие
        /// </summary>
        public virtual TicketInfoDb Ticket { get; set; }
        #endregion 
    }
}
