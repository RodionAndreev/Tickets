using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using Notification.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Web.Subscriber
{
    public class UserCreationMessageConsumer : IConsumeAsync<UserCreationMessage>
    {
        private IUserService m_service;
        private IMapper m_mapper;

        public UserCreationMessageConsumer(IUserService service, IMapper mapper)
        {
            m_service = service;
            m_mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "SubscribersNotificationMessage")]
        public async Task ConsumeAsync(UserCreationMessage message)
        {
            await m_service.CreateUser(message.UserId, message.FirstName, message.Email);
        }
    }
}
