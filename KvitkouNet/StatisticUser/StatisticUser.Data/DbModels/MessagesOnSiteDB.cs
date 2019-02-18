using StatisticUser.Data.DbModels.AbstractionsDB;

namespace StatisticUser.Data.DbModels
{
    /// <summary>
    /// Хранит статистику по количеству сообщений
    /// зарегистрированных пользователей
    /// </summary>
    public class MessagesUsersOnSiteDB: Entity<int>
    {
        public int UserId { get; set; }
        
        /// <summary>
        /// кол-во сообщений пользователя
        /// </summary>
        public int MessageCount { get; set; }
    }
}
