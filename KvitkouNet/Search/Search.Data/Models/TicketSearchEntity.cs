using System;

namespace Search.Data.Models
{
    /// <summary>
    /// Contains information about last ticket search.
    /// </summary>
    /// <seealso cref="Search.Data.Models.SearchEntity" />
    public class TicketSearchEntity : SearchEntity
    {
        /// <summary>
        /// Gets or sets the date from for search.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Gets or sets the date to for search.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// Gets or sets the category of the event for search.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the city where the event will be held for search.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the maximum price for search.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the minimum price for search.
        /// </summary>
        public decimal? MinPrice { get; set; }
    }
}
