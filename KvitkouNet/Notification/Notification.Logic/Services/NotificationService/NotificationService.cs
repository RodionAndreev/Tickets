using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Notification.Data.Context;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;
using Notification.Data.Models;
using Notification.Data.Models.Enums;
using Notification.Logic.Exceptions;

namespace Notification.Logic.Services.NotificationService
{
	public class NotificationService : INotificationService
	{
		private NotificationContext m_context;
		private IMapper m_mapper;

		public NotificationService(NotificationContext context, IMapper mapper)
		{
			m_context = context;
			m_mapper = mapper;
		}

		public async Task AddUserNotifications(UserNotificationBulkRequest request)
		{
			IQueryable<User> users = m_context.Users.Where(x => request.UserIds.Any(id => id == x.Id));
			Data.Models.Notification notification = m_mapper.Map<NotificationMessage, Data.Models.Notification>(request.Message);
            if (notification == null) throw new NotificationNotFound($"Уведомление не найдено");
            DateTime date = DateTime.UtcNow;          
            foreach (User user in users)
			{
                Data.Models.Notification notification2 = new Data.Models.Notification
                {
                    User = user,
                    Date = date,
                    IsClosed = false,
                    Type = NotificationType.Notification,
                    Text = notification.Text,
                    Title = notification.Title,
                    Severity = notification.Severity,
                    Creator = request.Message.Creator
                };
                m_context.Notifications.Add(notification2);
			}
            await m_context.SaveChangesAsync();			
		}

		public async Task EditNotification(UserNotification userNotification)
		{
			Data.Models.Notification oldNotification = await m_context.Notifications.SingleOrDefaultAsync(x => x.Id == userNotification.NotificationId);
            if (oldNotification == null) throw new NotificationNotFound($"Уведомление не найдено");
            m_mapper.Map(userNotification, oldNotification);

			//m_context.Attach(notification);
			//m_context.Entry(notification).State = EntityState.Modified;
			await m_context.SaveChangesAsync();
		}

		// проверочный метод
		public async Task<IEnumerable<UserNotification>> GetAll()
		{
			Data.Models.Notification[] result = m_context.Notifications.AsNoTracking()
				.Include(x => x.User).Where(x => x.IsClosed == false).ToArray();

            UserNotification[] mapped = m_mapper.Map<UserNotification[]>(result);

			return await Task.FromResult(mapped.AsEnumerable());
		}

		public async Task<UserNotification> GetNotification(string notificationId)
		{
            Data.Models.Notification notification = await m_context.Notifications
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == notificationId);
            if (notification == null) throw new NotificationNotFound($"Уведомление не найдено");
            return m_mapper.Map<UserNotification>(notification);
		}

		public async Task<IEnumerable<UserNotification>> GetUserNotifications(string userId, bool onlyOpen)
		{
            Data.Models.Notification[] result = m_context.Notifications.AsNoTracking()
				.Where(x => x.User.Id == userId && x.IsClosed != onlyOpen).ToArray();

			UserNotification[] mapped = m_mapper.Map<UserNotification[]>(result);

			return await Task.FromResult(mapped.AsEnumerable());
		}

		public async Task SetStatusClosed(string notificationId)
		{
			Data.Models.Notification notification = await m_context.Notifications.SingleOrDefaultAsync(x => x.Id == notificationId);
            if (notification == null) throw new NotificationNotFound($"Уведомление не найдено");
            notification.IsClosed = true;
			await m_context.SaveChangesAsync();
		}

		public void Dispose()
		{
			m_context.Dispose();
		}
	}
}