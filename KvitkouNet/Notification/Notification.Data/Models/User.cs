using System.Collections.Generic;

namespace Notification.Data.Models
{
	/// <summary>
	/// Пользователь
	/// </summary>
	public class User
	{
		/// <summary>
		/// Ид пользователя
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Электронный ящик
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Ник пользователя
		/// </summary>
		public string Name { get; set; }

        /// <summary>
        /// Уведомления
        /// </summary>
        public ICollection<Notification> Notifications { get; set; }

		/// <summary>
		/// Подписки пользователя
		/// </summary>
		public ICollection<UserSubscription> UserSubscriptions { get; set; }
	}
}
