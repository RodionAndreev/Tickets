using System;
using Notification.Data.Models.Enums;

namespace Notification.Data.Models
{
	/// <summary>
	/// Уведомление
	/// </summary>
	public class Notification
	{
		/// <summary>
		/// Id уведомления
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Id пользователя
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// Получатель уведомления
		/// </summary>
		public User User { get; set; }

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
		/// Дата отправки
		/// </summary>
		public DateTime Date { get; set; }

		/// <summary>
		/// Создатель уведомление
		/// </summary>
		public string Creator { get; set; }

        /// <summary>
        /// Отправлено на почту
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        public NotificationType Type { get; set; }

		/// <summary>
		/// Отметка для прочитанных уведомлений
		/// </summary>
		/// <remarks>для почтовых уведомлений будет закрыто, если успешно отправится</remarks>
		public bool IsClosed { get; set; }
	}
}