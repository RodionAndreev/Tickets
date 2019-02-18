using KvitkouNet.Messages.Notification.Enums;

namespace KvitkouNet.Messages.Notification
{
    /// <summary>
    /// Уведомления подписчиков
    /// </summary>
    /// <remarks>Сообщение SubscribersNotificationMessage</remarks>
    public class SubscribersNotificationMessage
    {
        /// <summary>
        /// Тема уведомления
        /// </summary>
        /// <remarks>Используется для объединения пользователей в группу подписки</remarks>
        public string Theme { get; set; }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Строгость уведомления
        /// </summary>
        public Severity Severity { get; set; }
    }
}
