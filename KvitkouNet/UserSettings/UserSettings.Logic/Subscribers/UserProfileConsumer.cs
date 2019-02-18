using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using System.Threading.Tasks;
using UserSettings.Logic.Services;

namespace UserSettings.Logic
{
	public class UserProfileConsumer : IConsumeAsync<UserCreationMessage>
	{
		private readonly IMapper _mapper;
		private readonly IUserSettingsService _userSettingsService;

		public UserProfileConsumer(IMapper mapper, IUserSettingsService userSettingsService)
		{
			_mapper = mapper;
			_userSettingsService = userSettingsService;
		}
		public async Task ConsumeAsync(UserCreationMessage message)
		{
			await _userSettingsService.CreateSetting(message.UserId);
		}
	}
}
