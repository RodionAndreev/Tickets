using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using AutoMapper;
using Notification.Data.Context;
using Notification.Logic.Services.Interfaces;
using Notification.Data.Models;
using Notification.Logic.Models.Requests;
using Notification.Logic.Configs;
using Notification.Logic.Models;

namespace Notification.Logic.Services.SubscriptionService
{
	public class SubscriptionService : ISubscriptionService
	{
		private NotificationContext m_context;
        INotificationService m_notificationService;
        IEmailNotificationService m_emailNotificationService;
        SenderConfig m_senderConfig;
        private IMapper m_mapper;

		public SubscriptionService(NotificationContext context, INotificationService notificationService, IEmailNotificationService emailNotificationService, IOptionsMonitor<SenderConfig> senderConfig, IMapper mapper)
		{
			m_context = context;
            m_notificationService = notificationService;
            m_emailNotificationService = emailNotificationService;
            m_senderConfig = senderConfig.CurrentValue;
            m_mapper = mapper;
		}

		public async Task<IEnumerable<Logic.Models.Subscription>> GetAll(string userId)
		{
            IQueryable<Data.Models.Subscription> subscriptions = m_context.UserSubscriptions
                .AsNoTracking()                
                .Include(x => x.Subscription)                
                .Where(x => x.UserId == userId).Select(x => x.Subscription);            
            
            return await Task.FromResult(m_mapper.Map<IEnumerable<Data.Models.Subscription>, IEnumerable<Logic.Models.Subscription>>(subscriptions));
		}

        public async Task NotifySubsribers(string theme, string text, Logic.Models.Enums.Severity severity)
        {
            Data.Models.Subscription subscription = await m_context.Subscriptions
                .AsNoTracking()
                .Include(x => x.UserSubscriptions.Where(u => u.IsSubscribed == true))
                .SingleOrDefaultAsync(x => x.Theme == theme);

            if (subscription == null) throw new System.Exception();

            NotificationMessage message = new NotificationMessage
            {
                Title = theme,
                Text = text,
                Severity = severity
            };

            UserNotificationBulkRequest notificationRequest = new UserNotificationBulkRequest
            {
                UserIds = subscription.UserSubscriptions.Where(x => x.ClientNotificationNeeded == true).Select(x => x.UserId),
                Message = message
            };

            UserNotificationBulkRequest emailRequest = new UserNotificationBulkRequest
            {
                UserIds = subscription.UserSubscriptions.Where(x => x.EmailNotificationNeeded == true).Select(x => x.UserId),
                Message = message
            };
            
            if (notificationRequest.UserIds.Count() != 0)
            {
                await m_notificationService.AddUserNotifications(notificationRequest);
            }
            if (emailRequest.UserIds.Count() != 0)
            {
                await m_emailNotificationService.SendEmailNotifications(emailRequest);
            }            
        }

        public async Task Subscribe(SubscriptionRequest request)
        {
            Data.Models.Subscription subscription = await m_context.Subscriptions.SingleOrDefaultAsync(x => x.Theme == request.Theme);
            if(subscription == null)
            {
                subscription = new Data.Models.Subscription { Theme = request.Theme, Creator = request.Creator };
            }

            if (subscription.Id == null || !(await m_context.UserSubscriptions.AnyAsync(x => x.UserId == request.UserId && x.SubscriptionId == subscription.Id)))
            {
                await m_context.UserSubscriptions.AddAsync(new UserSubscription
                {
                    UserId = request.UserId,
                    Subscription = subscription,
                    EmailNotificationNeeded = request.EmailNotificationNeeded,
                    ClientNotificationNeeded = request.ClientNotificationNeeded,                    
                    IsSubscribed = true
                });
                await m_context.SaveChangesAsync();
            }
        }

        public async Task Unsubscribe(UnsubscriptionRequest request)
        {
            Data.Models.Subscription subscription = await m_context.Subscriptions.AsNoTracking().SingleOrDefaultAsync(x => x.Theme == request.Theme);
            UserSubscription userSubscription = await m_context.UserSubscriptions
                .SingleOrDefaultAsync(x => x.UserId == request.UserId && x.SubscriptionId == subscription.Id);

            userSubscription.IsSubscribed = false;
            await m_context.SaveChangesAsync();
        }

		public void Dispose()
		{
			m_context.Dispose();
		}
	}
}
