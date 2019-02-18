using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Notification;
using KvitkouNet.Messages.Notification.Enums;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;
using Notification.Logic.Services;

namespace Notification.Web.Subscriber
{
    public class UserNotificationMessageConsumer : IConsumeAsync<UserNotificationMessage>
    {
        private INotificationService m_notificationService;
        private IEmailNotificationService m_emailService;
        private IMapper m_mapper;

        public UserNotificationMessageConsumer(INotificationService notificationService, IEmailNotificationService emailService, IMapper mapper)
        {
            m_notificationService = notificationService;
            m_emailService = emailService;
            m_mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserNotificationMessage")]
        public async Task ConsumeAsync(UserNotificationMessage message)
        {
            UserNotificationBulkRequest request = new UserNotificationBulkRequest
            {
                UserIds = new string[] { message.UserId },
                Message = new NotificationMessage
                {
                    Title = message.Title,
                    Text = message.Text,
                    Severity = m_mapper.Map<Logic.Models.Enums.Severity>(message.Severity)
                }
            };

            if (message.NotificationType == NotificationType.EmailNotification)
            {
                await m_emailService.SendEmailNotifications(request);
            }
            else if (message.NotificationType == NotificationType.Notification)
            {
                await m_notificationService.AddUserNotifications(request);
            }
        }
    }
}