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
    /// Контроллер для работы с логами о платежах
    /// </summary>
    [Route("api/logs/payments")]
    public class PaymentLogController : Controller
    {
        private readonly IPaymentLogService _loggingService;
        private bool _disposed;

        public PaymentLogController(IPaymentLogService loggingService)
        {
            _loggingService = loggingService;
        }

        /// <summary>
        /// Получает логи платежных операций по фильтру
        /// </summary>
        /// <param name="filter">Фильтр логов по платежным операциям</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<PaymentLogEntry>), Description = "Payment logs")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Invalid filter")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(string), Description = "Internal error")]
        public async Task<IActionResult> GetPaymentLogs([FromQuery] PaymentLogsFilter filter)
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
