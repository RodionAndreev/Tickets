namespace Search.Logic.Common.Models
{
    /// <summary>
    /// Contains information for user search request.
    /// </summary>
    /// <seealso cref="SearchRequest" />
    public class UserSearchRequest : SearchRequest
    {
        /// <summary>
        /// Gets or sets the minimum user rating for search.
        /// </summary>
        public double? MinRating { get; set; }
    }
}
