using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Notification.Logic.Models;
using Notification.Logic.Services;
using Notification.Logic.Models.Requests;
using Microsoft.AspNetCore.Cors;
using EasyNetQ;
using KvitkouNet.Messages.UserManagement;

namespace Notification.Web.Controllers
{
    /// <summary>
    /// api для уведомлений
    /// </summary>
    [Route("api/notification")]
	public class NotificationController : Controller
	{
		private INotificationService m_service;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="service"></param>
        public NotificationController(INotificationService service)
		{
			m_service = service;
		}

		/// <summary>
		/// Получить все уведомления
		/// </summary>
		/// <returns>возвращает список уведомлений для пользователя</returns>
		/// <remarks>email уведомления не входят</remarks>
		[HttpGet, Route("all")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<UserNotification>), Description = "All OK")]
		public async Task<IActionResult> GetAll()
		{			
			return Ok(await m_service.GetAll());
		}

		/// <summary>
		/// Получить уведомление
		/// </summary>
		/// <param name="id">ИД уведомления</param>
		/// <returns>возвращает уведомление для пользователя</returns>
		[HttpGet, Route("{id}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(UserNotification))]
		[SwaggerResponse(HttpStatusCode.Forbidden, typeof(void), Description = "Notification not found")]
		public async Task<IActionResult> GetNotification([FromRoute] string id)
		{
            return Ok(await m_service.GetNotification(id));
		}

		/// <summary>
		/// Обновить уведомление
		/// </summary>
		/// <param name="id">ИД уведомления</param>
		/// <param name="messsage">Сообщение уведомления</param>
		/// <remarks>email уведомления не обновляются</remarks>
		[HttpPatch, Route("{id}")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(NoContentResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResult))]
		public async Task<IActionResult> EditNotification([FromRoute] string id, [FromBody] NotificationMessage messsage)
		{
			await m_service.EditNotification(new UserNotification
			{
				NotificationId = id,
				Message = messsage
			});

			return NoContent();
		}

		/// <summary>
		/// Получить уведомления для пользователя
		/// </summary>
		/// <param name="id">ИД пользователя</param>
		/// <param name="onlyOpen">только незакрытые уведомления</param>
		/// <returns>возвращает список уведомлений для пользователя</returns>
		[HttpGet, Route("users/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<UserNotification>), Description = "All OK")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResult))]
        public async Task<IActionResult> GetUserNotifications([FromRoute] string id, [FromQuery] bool onlyOpen = true)
		{			
			return Ok(await m_service.GetUserNotifications(id, onlyOpen));
		}

		/// <summary>
		/// Создать уведомление для пользователя
		/// </summary>
		/// <param name="id">ИД пользователя</param>
		/// <param name="messsage">Сообщение уведомления</param>
		[HttpPost, Route("users/{id}")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(NoContentResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResult))]
        public async Task<IActionResult> AddUserNotification([FromRoute] string id, [FromBody] NotificationMessage messsage)
		{
			await m_service.AddUserNotifications(new UserNotificationBulkRequest
			{
				UserIds = new string[] { id },
				Message = messsage
			});
			return NoContent();
		}

		/// <summary>
		/// Отметить как прочитанное уведомление для пользователя
		/// </summary>
		[HttpDelete, Route("users/{id}")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(NoContentResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResult))]
        public async Task<IActionResult> SetStatusClosed([FromRoute] string id)
		{
			await m_service.SetStatusClosed(id);
			return NoContent();
		}

		/// <summary>
		/// Создать уведомления для пользователей
		/// </summary>
		/// <param name="request">Массовый запрос для пользователей</param>
		/// <remarks>Для ненайденных пользователей уведомление не будет создано</remarks>
		[HttpPost, Route("users/ids")]
		[SwaggerResponse(HttpStatusCode.NoContent, typeof(NoContentResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResult))]
        public async Task<IActionResult> AddNotifications([FromBody] UserNotificationBulkRequest request)
		{
			await m_service.AddUserNotifications(request);
			return NoContent();
		}		
	}
}