using System;

using StatisticUser.Data.DbModels.AbstractionsDB;

namespace StatisticUser.Data.DbModels
{
    public class SummaryTableDB: Entity<int>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int MessagesCount { get; set; }
        public int RatingPositive { get; set; }
        public int RatingNegative { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastOnWebsite { get; set; }
    }
}
