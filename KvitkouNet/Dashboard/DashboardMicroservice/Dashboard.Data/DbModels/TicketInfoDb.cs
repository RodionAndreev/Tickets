
using System;

namespace Dashboard.Data.DbModels
{
    /// <summary>
    ///     Класс описания доменной модели билета
    /// </summary>
    public class TicketInfoDb
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// Gets or sets the name of ticket.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the date of event.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the city where the event will be held.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the category of the event.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the price of the even.
        /// </summary>
        public decimal? Price { get; set; }

        #region Связи между таблицами 
        /// <summary>
        ///     Новость
        /// </summary>
        public virtual NewsDb NewsDbs { get; set; }
        #endregion 
    }
}
