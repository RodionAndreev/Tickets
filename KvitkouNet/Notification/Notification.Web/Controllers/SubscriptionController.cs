using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Notification.Logic.Models;
using Notification.Logic.Services;
using Notification.Logic.Services.Interfaces;
using Notification.Logic.Models.Requests;
using Microsoft.AspNetCore.Cors;

namespace Notification.Web.Controllers
{
    [Route("api/notification/subscription")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
		private ISubscriptionService m_subscriptionService;

		public SubscriptionController(ISubscriptionService subscriptionService)
		{
			m_subscriptionService = subscriptionService;
		}

		/// <summary>
		/// id пользователя
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet, Route("users/{id}")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Subscription>), Description = "All OK")]
		public async Task<IActionResult> GetAll([FromRoute]string id)
		{
			return Ok(await m_subscriptionService.GetAll(id));
		}


        [HttpPost, Route("test")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Subscription>), Description = "All OK")]
        public async Task<IActionResult> Subscribe([FromQuery] string id, [FromQuery] string theme)
        {
            await m_subscriptionService.Subscribe(new SubscriptionRequest { UserId = id, ClientNotificationNeeded = true, EmailNotificationNeeded = true, Theme = theme });
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id пользователя</param>
        /// <returns></returns>
        [HttpPost, Route("{theme}/users/{id}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<Subscription>), Description = "All OK")]
        public async Task<IActionResult> Unsubscribe([FromRoute] string id, [FromRoute] string theme)
        {
            await m_subscriptionService.Unsubscribe(new UnsubscriptionRequest { UserId = id, Theme = theme });
            return Ok();
        }

        
    }
}