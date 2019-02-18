namespace KvitkouNet.Messages.Notification
{
    /// <summary>
    /// Отписка пользователя от уведомления
    /// </summary>
    /// <remarks>Сообщение UserSubscriptionMessage.Unsubscribe</remarks>
    public class UserUnsubscriptionMessage
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        public string Theme { get; set; }
    }
}
