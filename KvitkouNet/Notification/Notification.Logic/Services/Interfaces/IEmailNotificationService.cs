using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;

namespace Notification.Logic.Services
{
	/// <summary>
	/// Интерфейс для почтовых уведомлений пользователя
	/// </summary>
	public interface IEmailNotificationService : IDisposable
	{
		/// <summary>
		/// Получить все email уведомления
		/// </summary>
		/// <returns>Список email уведомлений</returns>
		Task<IEnumerable<EmailNotification>> GetAllEmailNotifications();

		/// <summary>
		/// Получить email уведомления для пользователя
		/// </summary>
		/// <param name="userId">ИД пользователя</param>
		/// <returns>Список email уведомлений</returns>
		Task<IEnumerable<EmailNotification>> GetEmailNotifications(string userId);

		/// <summary>
		/// Отправить email уведомление для пользователей
		/// </summary>
		/// <param name="request">Массовая отправка сообщений пользователям</param>
		/// <returns></returns>
		Task SendEmailNotifications(UserNotificationBulkRequest request);

		/// <summary>
		/// Отправить email уведомление всем пользователям
		/// </summary>
		/// <param name="messsage">Cообщения уведомления</param> //перепилить в общую папку
		/// <returns></returns>
		Task SendEmailNotificationForAllUsers(NotificationMessage messsage);

		/// <summary>
		/// Отправляет сообщение для подтверждения регистрации
		/// </summary>
		/// <param name="sendEmailRequest">Запрос на отправку сообщения</param>
		/// <returns></returns>
		Task SendRegistrationNotification(SendEmailRequest sendEmailRequest);

		/// <summary>
		/// Подтверждение регистрации
		/// </summary>
		/// <param name="userName">Имя пользователя</param>
		/// <returns></returns>
		Task ConfirmRegistration(string userName);
	}
}
