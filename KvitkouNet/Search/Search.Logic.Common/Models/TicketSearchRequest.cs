using System;

namespace Search.Logic.Common.Models
{
    /// <summary>
    /// Contains information for ticket search request.
    /// </summary>
    /// <seealso cref="SearchRequest" />
    public class TicketSearchRequest : SearchRequest
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
        /// Gets or sets the name of the event.
        /// </summary>
        public string Name { get; set; }

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
