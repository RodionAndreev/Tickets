using System.Threading.Tasks;
using AutoMapper;
using Chat.Logic.Models;
using Chat.Logic.Services;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;

namespace Chat.Web.Subscriber
{
    public class UserMessageConsumer : IConsumeAsync<UserCreationMessage>
    {
        private IChatService m_service;
        private readonly IMapper _mapper;

        public UserMessageConsumer(IChatService service, IMapper mapper)
        {
            m_service = service;
            _mapper = mapper;
        }

        //добавим нового пользователя в системе к нам в базу
        [AutoSubscriberConsumer(SubscriptionId = "UserService.Created")]
        public Task ConsumeAsync(UserCreationMessage message)
        {
            var modelLogic = _mapper.Map<User>(message);

            return m_service.AddUser(modelLogic);

        }

        [AutoSubscriberConsumer(SubscriptionId = "UserService.Updated")]
        public Task ConsumeAsync(UserUpdatedMessage message)
        {
            var modelLogic = _mapper.Map<User>(message);

            return m_service.EditUser(modelLogic);

        }
    }
}