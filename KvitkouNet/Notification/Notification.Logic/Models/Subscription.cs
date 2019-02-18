using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Logic.Models
{
	/// <summary>
	/// Подписки
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
		/// Необходимо уведомление по почте
		/// </summary>
		public bool EmailNotificationNeeded { get; set; }
	}
}
