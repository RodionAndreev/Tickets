using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserSettings;
using Microsoft.EntityFrameworkCore;
using TicketManagement.Data.Context;
using TicketManagement.Data.DbModels;

namespace TicketManagement.Logic.Subscriber
{
    /// <summary>
    ///     Класс для получения сообщений о удалении пользователя из UserSettings
    /// </summary>
    public class UserDeleteMessageConsumerFromSettings : IConsumeAsync<DeleteUserProfileMessage>
    {
        private readonly IMapper _mapper;
        private readonly TicketContext _ticketContext;

        public UserDeleteMessageConsumerFromSettings(TicketContext ticketContext, IMapper mapper)
        {
            _ticketContext = ticketContext;
            _mapper = mapper;
        }

        public async Task ConsumeAsync(DeleteUserProfileMessage message)
        {
            var modelDb = _mapper.Map<UserInfo>(message);
            var userDb = await _ticketContext.UserInfos.FindAsync(modelDb.UserInfoId);
            if (userDb == null) return;
            var origin = _ticketContext.Tickets.Include(db => db.User)
                .Include(db => db.LocationEvent)
                .Include(db => db.SellerAdress)
                .Include(db => db.RespondedUsers)
                .Where(x => x.User.UserInfoId == modelDb.UserInfoId)
                .ToArray();
            _ticketContext.Tickets.RemoveRange(origin);
            await _ticketContext.SaveChangesAsync();
        }
    }
}