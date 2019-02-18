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
    /// Контроллер для работы с логами поисковых запросов
    /// </summary>
    [Route("api/logs/queries")]
    public class QueryLogController : Controller
    {
        private readonly ISearchLogService _loggingService;
        private bool _disposed;

        public QueryLogController(ISearchLogService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Получает логи поисковых запросов пользователей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр логов по поисковым запросам</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<SearchQueryLogEntry>), Description = "Search query logs")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid filter")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(string), Description = "Internal error")]
        public async Task<IActionResult> GetSearchQueryLogs([FromQuery] SearchQueryLogsFilter filter)
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
