using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserSettings;
using TicketManagement.Data.Context;
using TicketManagement.Data.DbModels;

namespace TicketManagement.Logic.Subscriber
{
    /// <summary>
    ///     Класс для получения сообщений о обновлении пользователя из UserSettings
    /// </summary>
    public class UserUpdateMessageConsumerFromSettings : IConsumeAsync<UserProfileUpdateMessage>
    {
        private readonly IMapper _mapper;
        private readonly TicketContext _ticketContext;

        public UserUpdateMessageConsumerFromSettings(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task ConsumeAsync(UserProfileUpdateMessage message)
        {
            var modelDb = _mapper.Map<UserInfo>(message);
            _ticketContext.UserInfos.Remove(modelDb);
            await _ticketContext.SaveChangesAsync();
        }
    }
}