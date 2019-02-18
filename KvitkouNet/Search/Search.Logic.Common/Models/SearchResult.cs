using System.Collections.Generic;

namespace Search.Logic.Common.Models
{
    /// <summary>
    /// Contains result information after search request.
    /// </summary>
    public class SearchResult<T>
    {
        /// <summary>
        /// Gets or sets the items after search request for 1 page.
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the total count of items after search request.
        /// </summary>
        public int Total { get; set; }
    }
}
