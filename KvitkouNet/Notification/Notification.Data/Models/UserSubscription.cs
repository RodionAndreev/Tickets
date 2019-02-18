namespace Notification.Data.Models
{
	/// <summary>
	/// Подписка пользователя
	/// </summary>
	public class UserSubscription
	{
		/// <summary>
		/// Id пользователя
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Пользователь
		/// </summary>
		public User User { get; set; }

		/// <summary>
		/// Id подписки
		/// </summary>
		public string SubscriptionId { get; set; }

		/// <summary>
		/// Подписка
		/// </summary>
		public Subscription Subscription { get; set; }

        /// <summary>
        /// Необходимо уведомление по почте
        /// </summary>
        public bool EmailNotificationNeeded { get; set; }

        /// <summary>
        /// Необходимо уведомление на клиенте 
        /// </summary>
        public bool ClientNotificationNeeded { get; set; }

        /// <summary>
        /// Подписан
        /// </summary>
        public bool IsSubscribed { get; set; }
	}
}