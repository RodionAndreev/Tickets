namespace Search.Data.Models
{
    /// <summary>
    /// Contains information about last user search.
    /// </summary>
    /// <seealso cref="Search.Data.Models.SearchEntity" />
    public class UserSearchEntity : SearchEntity
    {
        public double? MinRating { get; set; }
    }
}
