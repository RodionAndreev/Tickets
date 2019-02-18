using Notification.Logic.Models.Enums;

namespace Notification.Logic.Models
{
	/// <summary>
	/// Сообщение уведомления
	/// </summary>
	public class NotificationMessage
	{
        /// <summary>
        /// Создатель уведомления
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
	}	
}
