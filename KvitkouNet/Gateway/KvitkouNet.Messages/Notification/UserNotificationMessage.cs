using System;
using KvitkouNet.Messages.Notification.Enums;

namespace KvitkouNet.Messages.Notification
{
    /// <summary>
    /// Сообщение уведомления
    /// </summary>
    /// <remarks>Сообщение UserNotificationMessage</remarks>
    public class UserNotificationMessage
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Создатель уведомление
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Заголовок уведомления
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Строгость уведомления
        /// </summary>
        public Severity Severity { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        public NotificationType NotificationType { get; set; }
    }
}
