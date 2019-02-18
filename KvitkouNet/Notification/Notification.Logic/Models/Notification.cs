using System;

namespace Notification.Logic.Models
{
	/// <summary>
	/// Уведомление
	/// </summary>
	public class Notification
	{
		/// <summary>
		/// Id уведомления
		/// </summary>
		public string NotificationId { get; set; }

        /// <summary>
        /// Сообщение уведомления
        /// </summary>
        public NotificationMessage Message { get; set; }

		/// <summary>
		/// Дата отправки
		/// </summary>
		public DateTime Date { get; set; }
	}
}