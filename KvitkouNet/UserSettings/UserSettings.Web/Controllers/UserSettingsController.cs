using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using UserSettings.Logic.Models;
using UserSettings.Logic.Models.Helper;
using UserSettings.Logic.Services;
using UserSettings.Web.Models;

namespace UserSettings.Web.Controllers
{
	[Route("api/settings")]
	public class UserSettingsController : Controller
	{
		private IUserSettingsService _service;
		public UserSettingsController(IUserSettingsService service)
		{
			_service = service;
		}

		/// <summary>
		/// Запрос на изменение основных данных пользователя
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut, Route("{id}/userinfo")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "All OK")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> UpdateProfile([FromBody]ProfileDto model, [FromRoute] string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			ResultEnum result = await _service.UpdateProfile(id, model.FirstName, model.MiddleName, model.LastName, model.Birthday);
			return result == ResultEnum.Success ? (IActionResult)Ok(result) : BadRequest();
		}

		/// <summary>
		/// Запрос на изменение пароля
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut, Route("{id}/password")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "All OK")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> UpdatePassword([FromBody]PasswordDto model, [FromRoute] string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			ResultEnum result = await _service.UpdatePassword(id, model.OldPassword, model.NewPassword, model.ConfirmPassword);
			return result == ResultEnum.Success ? (IActionResult)Ok(result) : BadRequest();
		}

		/// <summary>
		/// Запрос на изменение email
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		[HttpPut, Route("{id}/email")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "All OK")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> UpdateEmail([FromBody]string email, [FromRoute]string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			ResultEnum result = await _service.UpdateEmail(id, email);
			return result == ResultEnum.Success ? (IActionResult)Ok(result) : BadRequest();
		}

		/// <summary>
		/// Запрос на изменение уведомлений
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut, Route("{id}/notifications")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "All OK")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> UpdateNotification([FromBody]Notifications notification, [FromRoute]string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			ResultEnum result = await _service.UpdateNotifications(id, notification);
			return result == ResultEnum.Success ? (IActionResult)Ok(result) : BadRequest();
		}

		[HttpPut, Route("{id}/visible")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "All OK")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> UpdateVisibleInformation([FromBody]VisibleInfo visibleInfo, [FromRoute]string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			ResultEnum result = await _service.UpdateVisible(id, visibleInfo);
			return result == ResultEnum.Success ? (IActionResult)Ok(result) : BadRequest();
		}

		/// <summary>
		/// Запрос на удаление аккаунта
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut, Route("{id}/delete")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(void), Description = "All OK")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> DeleteAccount([FromRoute] string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			var result = await _service.DeleteAccount(id);
			return result ? (IActionResult)Ok(result) : BadRequest();
		}

		/// <summary>
		/// Получение данных профиля
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(Settings), Description = "All Ok")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> Get([FromRoute] string id)
		{
			if (string.IsNullOrEmpty(id))
				return BadRequest();
			var result = await _service.Get(id);
			return Ok(result);
		}

		[HttpPost("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(Settings), Description = "All Ok")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid model")]
		public async Task<IActionResult> CreateSettings(string id)
		{
			if (string.IsNullOrEmpty(id)) return BadRequest();
			ResultEnum result = await _service.CreateSetting(id);
			return result == ResultEnum.Success ? (IActionResult)Ok() : BadRequest();
		}

		[HttpPut, Route("{id}/update")]
		public async Task<IActionResult> UpdateSettings([FromRoute] string id, [FromBody]Settings model)
		{
			if (string.IsNullOrEmpty(id)) return BadRequest();
			ResultEnum result = await _service.UpdateSettings(id, model.IsPrivateAccount, model.IsGetTicketInfo);
			return result == ResultEnum.Success ? (IActionResult)Ok() : BadRequest();
		}
	}
}