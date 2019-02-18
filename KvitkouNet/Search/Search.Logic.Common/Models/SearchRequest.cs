namespace Search.Logic.Common.Models
{
    /// <summary>
    /// Contains common information for search request.
    /// </summary>
    public abstract class SearchRequest
    {
        /// <summary>
        /// Gets or sets the offset from the beginning of the result set.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the maximal number of items to return in the response.
        /// </summary>
        public int Limit { get; set; }
    }
}
