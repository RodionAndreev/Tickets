using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;

namespace Notification.Logic.Services
{
	/// <summary>
	/// Интерфейс для работы с уведомлениями
	/// </summary>
	public interface INotificationService : IDisposable
	{
		/// <summary>
		/// Получить все уведомления
		/// </summary>
		/// <returns>возвращает список уведомлений для пользователя</returns>
		/// <remarks>email уведомления не входят</remarks>
		Task<IEnumerable<UserNotification>> GetAll();

		/// <summary>
		/// Получить уведомление
		/// </summary>
		/// <param name="notificationId">ИД уведомления</param>
		/// <returns>возвращает уведомление для пользователя</returns>
		Task<UserNotification> GetNotification(string notificationId);

		/// <summary>
		/// Обновить уведомление
		/// </summary>
		/// <param name="userNotification">параметры уведомления</param>
		/// <remarks>email уведомления не обновляются</remarks>
		Task EditNotification(UserNotification userNotification);

		/// <summary>
		/// Получить уведомления для пользователя
		/// </summary>
		/// <param name="userId">ИД пользователя</param>
		/// <param name="onlyOpen">Признак действующий уведомлений</param>
		/// <returns>возвращает список уведомлений для пользователя</returns>
		Task<IEnumerable<UserNotification>> GetUserNotifications(string userId, bool onlyOpen);

		/// <summary>
		/// Создать уведомление для пользователей
		/// </summary>
		/// <param name="request">Запрос для уведомления</param>
		/// <remarks>Для ненайденных пользователей уведомление не будет создано</remarks>
		Task AddUserNotifications(UserNotificationBulkRequest request);

		/// <summary>
		/// Отметить как прочитанное уведомление для пользователя
		/// </summary>
		/// <param name="notificationId">ИД уведомления</param>
		Task SetStatusClosed(string notificationId);
	}
}