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
    /// Контроллер для работы с логами о действиях с билетами
    /// </summary>
    [Route("api/logs/tickets")]
    public class TicketActionLogController : Controller
    {
        private readonly ITicketLogService _loggingService;
        private bool _disposed;

        public TicketActionLogController(ITicketLogService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Получает логи действий с билетами по фильтру
        /// </summary>
        /// <param name="filter">Фильтр логов по билетам</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<TicketActionLogEntry>), Description = "Ticket action logs")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid filter")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(string), Description = "Internal error")]
        public async Task<IActionResult> GetTicketActionLogs([FromQuery] TicketLogsFilter filter)
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
