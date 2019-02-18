using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Notification;
using Notification.Logic.Models.Requests;
using Notification.Logic.Services.Interfaces;
using System.Threading.Tasks;

namespace Notification.Web.Subscriber
{
    public class UserSubscriptionMessageConsumer : IConsumeAsync<UserSubscriptionMessage>
    {
        private ISubscriptionService m_service;
        private IMapper m_mapper;

        public UserSubscriptionMessageConsumer(ISubscriptionService service, IMapper mapper)
        {
            m_service = service;
            m_mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserSubscriptionMessage.Subscribe")]
        public async Task ConsumeAsync(UserSubscriptionMessage message)
        {
            await m_service.Subscribe(m_mapper.Map<SubscriptionRequest>(message));
        }
    }
}
