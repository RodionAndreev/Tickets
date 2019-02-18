using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Notification.Logic.Models;
using Notification.Logic.Services;
using Notification.Logic.Configs;
using Microsoft.Extensions.Options;
using EasyNetQ;
using KvitkouNet.Messages.Notification;

namespace Notification.Web.Controllers
{
    [Route("api/notification/email")]
    [ApiController]
    public class EmailNotificationController : ControllerBase
    {
		private IEmailNotificationService m_emailService;
        private IBus m_bus;
        private SenderConfig m_config;

        public EmailNotificationController(IEmailNotificationService emailService, IBus bus, IOptionsMonitor<SenderConfig> config)
		{
			m_emailService = emailService;
            m_bus = bus;
            m_config = config.CurrentValue;
        }
			   
		/// <summary>
		/// Получить все email уведомления
		/// </summary>
		/// <returns>Список email уведомлений</returns>
		[HttpGet, Route("all")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<EmailNotification>))]
		public async Task<IActionResult> GetAllEmailNotifications()
		{
			return Ok(await m_emailService.GetAllEmailNotifications());
		}

		/// <summary>
		/// Получить email уведомления для пользователя
		/// </summary>
		/// <param name="id">ИД пользователя</param>
		/// <returns>Список email уведомлений</returns>
		[HttpGet, Route("users/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<EmailNotification>), Description = "All OK")]
		public async Task<IActionResult> GetEmailNotifications([FromRoute] string id)
		{
			return Ok(await m_emailService.GetEmailNotifications(id));
		}

        /// <summary>
        /// Подтвержение регистрации
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        [HttpPost, Route("registration/confirmation/{uname}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(NoContentResult))]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestObjectResult))]
		public async Task<IActionResult> ConfirmRegistration([FromRoute] string uname)
		{
			await m_emailService.ConfirmRegistration(uname);
            m_bus.Publish(new ConfirmRegistrationMessage
            {
                Name = uname
            });
			return NoContent();
		}
	}
}