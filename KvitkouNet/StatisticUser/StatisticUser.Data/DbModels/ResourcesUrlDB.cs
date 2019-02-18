using StatisticUser.Data.DbModels.AbstractionsDB;

namespace StatisticUser.Data.DbModels
{
    /// <summary>
    /// Список ресурсов сайта в строковом виде
    /// которые запрашивали пользователи
    /// </summary>
    public class ResourcesUrlDB: Entity<int>
    {
        public int ResourceUrl { get; set; }
    }
}
