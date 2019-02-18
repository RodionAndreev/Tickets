using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using Security.Logic.Services;

namespace Security.Web.Subscriber
{
    public class DeleteUserNotificationMessageConsumer : IConsumeAsync<UserDeletedMessage>
    {
        private IUserRightsService m_service;

        public DeleteUserNotificationMessageConsumer(IUserRightsService service)
        {
            m_service = service;
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserDeletedMessage")]
        public async Task ConsumeAsync(UserDeletedMessage message)
        {
            await m_service.EditUserRights(message.UserId, new int[0], new int[0], new int[0], new int[0]);
        }
    }
}
