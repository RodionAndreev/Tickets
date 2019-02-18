using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EasyNetQ;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;
using Logging.Logic.Models.Filters;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Logging.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с логами об ошибках
	/// </summary>
	[Route("api/logs/errors")]
	public class ErrorLogController : Controller
	{
		private readonly IErrorLogService _loggingService;

		private bool _disposed;

		public ErrorLogController(IErrorLogService loggingService, IBus bus)
		{
			_loggingService = loggingService;
		}

		/// <summary>
		/// Получает логи ошибок сервера по фильтру
		/// </summary>
		/// <param name="filter">Фильтр логов ошибок сервера</param>
		/// <returns></returns>
		[HttpGet]
		[Route("")]
		[SwaggerResponse(HttpStatusCode.OK, typeof(IEnumerable<InternalErrorLogEntry>), Description = "Error logs")]
		[SwaggerResponse(HttpStatusCode.BadRequest, typeof(string), Description = "Validation error")]
		[SwaggerResponse(HttpStatusCode.InternalServerError, typeof(string), Description = "Internal error")]
		public async Task<IActionResult> GetErrorLogs([FromQuery] ErrorLogsFilter filter)
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
