namespace Notification.Logic.Models.Requests
{
    /// <summary>
    /// Запрос на подписку пользователя
    /// </summary>
    public class SubscriptionRequest
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public string UserId { get; set; }
        
        /// <summary>
        /// Создатель подписки
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        /// <remarks>Если темы не существует, то добавится</remarks>
        public string Theme { get; set; }

        /// <summary>
        /// Необходимо уведомление по почте
        /// </summary>
        public bool EmailNotificationNeeded { get; set; }

        /// <summary>
        /// Необходимо уведомление на клиенте 
        /// </summary>
        public bool ClientNotificationNeeded { get; set; }
    }
}
