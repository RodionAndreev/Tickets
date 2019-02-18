using System;
using System.Collections.Generic;
using System.Text;
using StatisticUser.Data.DbModels.AbstractionsDB;

namespace StatisticUser.Data.DbModels
{
    /// <summary>
    /// Содержит с рейтинг пользователя
    /// Негативный и позитивный.
    /// Может быть представлен как отношение Positive к Negative
    /// или как разница этих величин и т.д.
    /// </summary>
    public class RatingDB: Entity<int>
    {
        public int UserId { get; set; }
        public int Positive { get; set; }
        public int Negative { get; set; }
    }
}
