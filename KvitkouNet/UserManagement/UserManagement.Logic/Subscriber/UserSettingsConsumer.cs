using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Notification;
using KvitkouNet.Messages.TicketManagement;
using KvitkouNet.Messages.UserManagement;
using KvitkouNet.Messages.UserSettings;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UserManagement.Logic.Services;

namespace UserManagement.Logic.Subscriber
{
    public class UserSettingsMessageConsumer :
        IConsumeAsync<PasswordUpdateMessage>,
        IConsumeAsync<DeleteUserProfileMessage>,
        IConsumeAsync<ConfirmRegistrationMessage>

    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserSettingsMessageConsumer(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
        }

        public Task ConsumeAsync(PasswordUpdateMessage message)
        {
            throw new NotImplementedException();
        }

        public Task ConsumeAsync(DeleteUserProfileMessage message)
        {
            throw new NotImplementedException();
        }

        public async Task ConsumeAsync(ConfirmRegistrationMessage message)
        {
            await _userService.UpdateEmailStatus(message.Name);
        }
    }
}
