using System.Collections.Generic;

namespace Notification.Logic.Models.Requests
{
	/// <summary>
	/// Запрос для массового добавления уведомленний
	/// </summary>
	public class UserNotificationBulkRequest
	{
		/// <summary>
		/// ИД пользователей
		/// </summary>
		public IEnumerable<string> UserIds { get; set; }

		/// <summary>
		/// Сообщение уведомления
		/// </summary>
		public NotificationMessage Message { get;set;}
	}
}
