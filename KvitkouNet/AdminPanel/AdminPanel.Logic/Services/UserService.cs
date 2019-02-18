using System.Threading.Tasks;
using AdminPanel.Logic.Dtos.UserManagement;
using AdminPanel.Logic.Generated.UserManagement;
using AdminPanel.Logic.Infrastructure;

namespace AdminPanel.Logic.Services
{
	public class UserServiceWrapper : IUserServiceWrapper
	{
		private readonly IUser _userService;

		public UserServiceWrapper(IUser userService)
		{
			_userService = userService;
		}

		public async Task<object> GetAll()
		{
			return await _userService.GetAllAsync();
		}

		public Task ChangeIsBannedStatus(IsBannedDto dto)
		{
			throw new System.NotImplementedException();
		}
	}
}