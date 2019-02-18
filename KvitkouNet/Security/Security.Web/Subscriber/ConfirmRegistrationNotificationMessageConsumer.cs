using System.Linq;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Notification;
using Microsoft.Extensions.Configuration;
using Security.Logic.Services;
using Security.Web.ConfigModels;

namespace Security.Web.Subscriber
{
    public class ConfirmRegistrationNotificationMessageConsumer : IConsumeAsync<ConfirmRegistrationMessage>
    {
        private IUserRightsService m_service;
        private IConfiguration m_config;

        public ConfirmRegistrationNotificationMessageConsumer(IUserRightsService service, IConfiguration config)
        {
            m_service = service;
            m_config = config;
        }

        [AutoSubscriberConsumer(SubscriptionId = "ConfirmRegistrationMessage")]
        public async Task ConsumeAsync(ConfirmRegistrationMessage message)
        {
            var confirmedUserRights = m_config.GetSection("ConfirmedUserRights").Get<DefaultUserRights>();
            
            await m_service.EditUserRightsByNames(message.Name,
                confirmedUserRights.Roles.Select(l => l.Name).ToArray(),
                confirmedUserRights.Functions.Select(l => l.Name).ToArray(),
                confirmedUserRights.AccessRights.Select(l => l.Name).ToArray(),
                confirmedUserRights.DeniedRights.Select(l => l.Name).ToArray());
        }
    }
}
