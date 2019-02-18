using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notification.Data.Context;
using Notification.Data.Models;
using Notification.Logic.Configs;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;
using Notification.Data.Models.Enums;
using Microsoft.Extensions.Options;
using Notification.Logic.Exceptions;

namespace Notification.Logic.Services.EmailNotificationService
{
	public class EmailNotificationService : IEmailNotificationService
	{
		private NotificationContext m_context;
		private IMapper m_mapper;
		private IEmailSenderService m_emailSenderService;
		private SenderConfig m_senderConfig;

		public EmailNotificationService(NotificationContext context, IEmailSenderService emailSenderService, IMapper mapper, IOptionsMonitor<SenderConfig> senderConfig)
		{
			m_context = context;
			m_emailSenderService = emailSenderService;
			m_mapper = mapper;
			m_senderConfig = senderConfig.CurrentValue;
		}

		public async Task<IEnumerable<EmailNotification>> GetAllEmailNotifications()
		{
			var result = m_context.Notifications
				.AsNoTracking()
				.Where(x => x.Type == NotificationType.EmailNotification);

			var mapped = m_mapper.Map<IEnumerable<Data.Models.Notification>, IEnumerable<EmailNotification>>(result);

			return await Task.FromResult(mapped.AsEnumerable());
		}

		public async Task<IEnumerable<EmailNotification>> GetEmailNotifications(string userId)
		{
			var result = m_context.Notifications
				.AsNoTracking()
				.Where(x => x.Type == NotificationType.EmailNotification && x.User.Id == userId);

			var mapped = m_mapper.Map<IEnumerable<Data.Models.Notification>, IEnumerable<EmailNotification>>(result);

			return await Task.FromResult(mapped.AsEnumerable());
		}

		public async Task SendEmailNotificationForAllUsers(NotificationMessage messsage)
		{
			IQueryable<User> users = m_context.Users.AsNoTracking();
			await SendEmailForUsers(users, messsage.Creator, messsage.Title, messsage.Text, m_mapper.Map<Data.Models.Enums.Severity>(messsage.Severity));
		}

		public async Task SendEmailNotifications(UserNotificationBulkRequest request)
		{
			IQueryable<User> users = m_context.Users.AsNoTracking().Where(x => request.UserIds.Any(id => id == x.Id));
			await SendEmailForUsers(users, request.Message.Creator, request.Message.Title, request.Message.Text, m_mapper.Map<Data.Models.Enums.Severity>(request.Message.Severity));		
		}

		public async Task SendRegistrationNotification(SendEmailRequest sendEmailRequest)
		{
			//валидация
			User user = await m_context.Users.SingleOrDefaultAsync(x => x.Name == sendEmailRequest.ReceiverName);

			if (user != null) throw new UserNotFound($"Пользователь {sendEmailRequest.ReceiverName} не найден");

			await m_emailSenderService.SendEmailAsync(sendEmailRequest, m_senderConfig);

			if (await m_context.TemporaryUsers.SingleOrDefaultAsync(x => x.Name == sendEmailRequest.ReceiverName) == null)
			{
				await m_context.TemporaryUsers.AddAsync(new TemporaryUser
				{
					Name = sendEmailRequest.ReceiverName,
					Email = sendEmailRequest.ReceiverEmail
				});
			}

			await m_context.SaveChangesAsync();
		}

		public async Task ConfirmRegistration(string userName)
		{
			TemporaryUser user = await m_context.TemporaryUsers.SingleOrDefaultAsync(x => x.Name == userName);

			if (user == null) throw new UserNotFound($"Пользователь {userName} не найден");

			m_context.TemporaryUsers.Remove(user);

			await m_context.SaveChangesAsync();
		}

		private async Task SendEmailForUsers(IEnumerable<User> users, string creator, string subject, string text, Data.Models.Enums.Severity severity)
		{
            foreach (User user in users)
			{
				await m_emailSenderService.SendEmailAsync(new SendEmailRequest
				{
					ReceiverName = user.Name,
					ReceiverEmail = user.Email,
                    Subject = subject + GetSeverityMessage(severity),
                    Text = text
				}, m_senderConfig);

                await m_context.Notifications.AddAsync(new Data.Models.Notification
                {
                    Date = DateTime.UtcNow,
                    UserId = user.Id,
                    Creator = creator,
                    Title = subject,
                    Text = text,
                    Severity = severity,
                    Type = NotificationType.EmailNotification,
                    IsClosed = true,
                    Email = user.Email
                });
			}
            await m_context.SaveChangesAsync();
		}

        private string GetSeverityMessage(Data.Models.Enums.Severity severity)
        {
            if (severity == Data.Models.Enums.Severity.Informational) return " [Информирование]";
            else if (severity == Data.Models.Enums.Severity.Warning) return " [Предупреждение]";
            else if (severity == Data.Models.Enums.Severity.Error) return " [Ошибка]";
            return string.Empty;
        }

		public void Dispose()
		{
			m_context.Dispose();
		}	
	}
}