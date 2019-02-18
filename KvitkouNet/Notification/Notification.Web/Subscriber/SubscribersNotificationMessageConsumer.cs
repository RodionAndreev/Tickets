using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Notification;
using Notification.Logic.Services.Interfaces;
using System.Threading.Tasks;

namespace Notification.Web.Subscriber
{
    public class SubscribersNotificationMessageConsumer : IConsumeAsync<SubscribersNotificationMessage>
    {
        private ISubscriptionService m_service;
        private IMapper m_mapper;

        public SubscribersNotificationMessageConsumer(ISubscriptionService service, IMapper mapper)
        {
            m_service = service;
            m_mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "SubscribersNotificationMessage")]
        public async Task ConsumeAsync(SubscribersNotificationMessage message)
        {
            await m_service.NotifySubsribers(message.Theme, message.Text, m_mapper.Map<Logic.Models.Enums.Severity>(message.Severity));
        }
    }
}
