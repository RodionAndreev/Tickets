namespace KvitkouNet.Messages.Notification
{
    /// <summary>
    /// Подписка пользователя на уведомления
    /// </summary>
    /// <remarks>Сообщение UserSubscriptionMessage.Subscribe</remarks>
    public class UserSubscriptionMessage
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
