using System;
using StatisticUser.Data.DbModels.AbstractionsDB;

namespace StatisticUser.Data.DbModels
{
    /// <summary>
    /// Статистика посещения ресурсов сайта
    /// TimeOnResource время определяется от последней активности
    /// из таблицы TimeOnSite
    /// </summary>
    public class OpenResourcesDb: Entity<string>
    {
        /// <summary>
        /// id ResourcesUrlDB
        /// </summary>
        public ResourcesUrlDB ResourceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TimeOnResource { get; set; }
    }
}
