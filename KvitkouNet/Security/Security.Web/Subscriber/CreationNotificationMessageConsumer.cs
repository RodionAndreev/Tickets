using System.Linq;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using Microsoft.Extensions.Configuration;
using Security.Logic.Models;
using Security.Logic.Services;
using Security.Web.ConfigModels;

namespace Security.Web.Subscriber
{
    public class CreationNotificationMessageConsumer : IConsumeAsync<UserCreationMessage>
    {
        private IUserRightsService m_service;
        private IConfiguration m_config;

        public CreationNotificationMessageConsumer(IUserRightsService service, IConfiguration config)
        {
            m_service = service;
            m_config = config;
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserCreationMessage")]
        public async Task ConsumeAsync(UserCreationMessage message)
        {
            var defaultUserRights = m_config.GetSection("DefaultUserRights").Get<DefaultUserRights>();

            await m_service.SetDefaultRoleToNewUser(new UserInfo
                {
                    UserLogin = message.Email,
                    UserId = message.Email,
                    FirstName = message.FirstName,
                    LastName = message.LastName
                },
                new UserRights
                {
                    Roles = defaultUserRights.Roles,
                    AccessFunctions = defaultUserRights.Functions,
                    AccessRights = defaultUserRights.AccessRights,
                    DeniedRights = defaultUserRights.DeniedRights
                }
            );
        }
    }
}
