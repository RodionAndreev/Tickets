using System;

namespace Search.Data.Models
{
    /// <summary>
    /// Contains common information about last search.
    /// </summary>
    public abstract class SearchEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the search time.
        /// </summary>
        public DateTime SearchTime { get; set; }
    }
}
