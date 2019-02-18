using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using Security.Logic.Models;
using Security.Logic.Services;

namespace Security.Web.Subscriber
{
    public class UpdateUserNotificationMessageConsumer : IConsumeAsync<UserUpdatedMessage>
    {
        private IUserRightsService m_service;

        public UpdateUserNotificationMessageConsumer(IUserRightsService service)
        {
            m_service = service;
        }

        [AutoSubscriberConsumer(SubscriptionId = "RegistrationMessage")]
        public async Task ConsumeAsync(UserUpdatedMessage message)
        {
            await m_service.UpdateUserInfo(new UserInfo
                {
                    LastName = message.LastName,
                    FirstName = message.FirstName
                }
            );
        }
    }
}
