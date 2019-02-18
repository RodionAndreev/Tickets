namespace Search.Logic.Common.Models
{
    /// <summary>
    /// Contains minimal information about user indexed by Elasticsearch
    /// </summary>
    public class UserInfo : EntityInfo
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the user rating.
        /// </summary>
        public double Rating { get; set; }
    }
}
