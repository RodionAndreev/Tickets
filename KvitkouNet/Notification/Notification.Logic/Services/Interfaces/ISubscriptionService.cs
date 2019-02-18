using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;

namespace Notification.Logic.Services.Interfaces
{
    /// <summary>
    /// Сервис подписок
    /// </summary>
	public interface ISubscriptionService : IDisposable
	{
        /// <summary>
        /// Получить все подписки
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
		Task<IEnumerable<Subscription>> GetAll(string userId);

        /// <summary>
        /// Отправка уведомления для подписчиков
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        Task NotifySubsribers(string theme, string text, Logic.Models.Enums.Severity severity);

        /// <summary>
        /// Подписка пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Subscribe(SubscriptionRequest request);

        /// <summary>
        /// Отписка пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task Unsubscribe(UnsubscriptionRequest request);
	}
}
