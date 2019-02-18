using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;
using Logging.Logic.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Logging.Web.Controllers
{
    /// <summary>
    /// Контроллер для работы с логами действий с аккаунтами пользователей
    /// </summary>
    [Route("api/logs/accounts")]
    public class AccountLogController : Controller
    {
        private readonly IAccountLogService _loggingService;
        private bool _disposed;

        public AccountLogController(IAccountLogService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Получает логи действий с аккаунтами пользователей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр логов действий с аккаунтами</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<AccountLogEntry>), Description = "Account logs")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid filter")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(string), Description = "Internal error")]
        public async Task<IActionResult> GetAccountLogs([FromQuery] AccountLogsFilter filter)
        {
            var result = await _loggingService.GetLogsAsync(filter);

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _loggingService?.Dispose();
            }

            _disposed = true;

            base.Dispose(disposing);
        }
    }
}
