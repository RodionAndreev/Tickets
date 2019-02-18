using System.Threading.Tasks;
using AdminPanel.Logic.Dtos.UserManagement;
using AdminPanel.Logic.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с пользователями через панель администратора
	/// </summary>
	[Route("api/admin/users")]
	public class UserController : Controller
	{
		private readonly IUserServiceWrapper _userService;

		public UserController(IUserServiceWrapper userService)
		{
			_userService = userService;
		}

		/// <summary>
		/// Получает пользователей для просмотра с помощью панели администратора
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _userService.GetAll();
			return Ok(result);
		}

		/// <summary>
		/// Банит или снимает бан с пользователя с помощью панели администратора
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		[HttpPatch]
		[Route("ban/")]
		public async Task<IActionResult> ChangeIsBannedStatus([FromBody] IsBannedDto dto)
		{
			await _userService.ChangeIsBannedStatus(dto);
			return NoContent();
		}
	}
}