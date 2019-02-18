using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Data.Models
{
	/// <summary>
	/// 
	/// </summary>
	public class Subscription
	{
		/// <summary>
		/// Id подписки
		/// </summary>
		public string Id { get; set; }

        /// <summary>
        /// Создатель подписки
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        public string Theme { get; set; }


		/// <summary>
		/// Подписки для пользователя
		/// </summary>
		public ICollection<UserSubscription> UserSubscriptions { get; set; }
	}
}