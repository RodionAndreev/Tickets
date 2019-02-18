using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using Search.Data.Repositories;
using Search.Logic.Common.Models;

namespace Search.Web.Subscriber
{
    public class UserMessageConsumer: IConsumeAsync<UserCreationMessage>, IConsumeAsync<UserUpdatedMessage>, IConsumeAsync<UserDeletedMessage>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserMessageConsumer(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserService.Created")]
        public async Task ConsumeAsync(UserCreationMessage message)
        {
           await _userRepository.SaveAsync(_mapper.Map<UserInfo>(message)).ConfigureAwait(false);
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserService.Updated")]
        public async Task ConsumeAsync(UserUpdatedMessage message)
        {
            await _userRepository.SaveAsync(_mapper.Map<UserInfo>(message)).ConfigureAwait(false);
        }

        [AutoSubscriberConsumer(SubscriptionId = "UserService.Deleted")]
        public async Task ConsumeAsync(UserDeletedMessage message)
        {
            await _userRepository.DeleteAsync(message.UserId);
        }
    }
}
