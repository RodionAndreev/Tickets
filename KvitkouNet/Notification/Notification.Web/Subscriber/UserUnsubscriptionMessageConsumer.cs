using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Notification;
using Notification.Logic.Models.Requests;
using Notification.Logic.Services.Interfaces;
using System.Threading.Tasks;

namespace Notification.Web.Subscriber
{
    public class UserUnsubscriptionMessageConsumer : IConsumeAsync<UserUnsubscriptionMessage>
    {
        private ISubscriptionService m_service;
        private IMapper m_mapper;

        public UserUnsubscriptionMessageConsumer(ISubscriptionService service, IMapper mapper)
        {
            m_service = service;
            m_mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserSubscriptionMessage.Unsubscribe")]
        public async Task ConsumeAsync(UserUnsubscriptionMessage message)
        {
            await m_service.Unsubscribe(m_mapper.Map<UnsubscriptionRequest>(message));
        }
    }
}
