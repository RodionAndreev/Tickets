using System;
using System.Net.Http;
using System.Threading.Tasks;
using AdminPanel.Logic.Generated.Logging;
using AdminPanel.Web.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Rest;

namespace AdminPanel.Web.Controllers
{
	/// <summary>
	/// Контроллер для работы с логами через панель администратора
	/// </summary>
	[Route("api/admin/logs")]
	[ServiceFilter(typeof(GlobalExceptionFilter))]
	public class LoggingController : Controller
	{
		private readonly IErrorLog _errorLogService;
		private readonly IAccountLog _accountLogService;
		private readonly IPaymentLog _paymentLogService;
		private readonly IQueryLog _queryLogService;
		private readonly ITicketActionLog _ticketActionLogService;
		private readonly ITicketDealLog _ticketDealLogService;

		public LoggingController(IErrorLog errorLogService,
			IAccountLog accountLogService,
			IPaymentLog paymentLogService,
			IQueryLog queryLogService,
			ITicketActionLog ticketActionLogService,
			ITicketDealLog ticketDealLogService)
		{
			_errorLogService = errorLogService;
			_accountLogService = accountLogService;
			_paymentLogService = paymentLogService;
			_queryLogService = queryLogService;
			_ticketActionLogService = ticketActionLogService;
			_ticketDealLogService = ticketDealLogService;
		}

		/// <summary>
		/// Возвращает список залогированных ошибок
		/// </summary>
		/// <returns></returns>
		[HttpGet("errors")]
		public async Task<IActionResult> GetErrorLogs(
			[FromQuery] string serviceName,
			string exceptionTypeName,
			string message,
			DateTime? dateFrom,
			DateTime? dateTo)
		{
			try
			{
				return Ok(await _errorLogService.GetErrorLogsAsync(serviceName: serviceName,
					exceptionTypeName:exceptionTypeName,
					message:message,
					dateFrom:dateFrom,
					dateTo:dateTo));
			}
			catch (SerializationException e)
			{
				return BadRequest($"{e.Message} : {e.Content}");
			}
		}

		/// <summary>
		/// Возвращает список логов по действиям с аккаунтом
		/// </summary>
		/// <returns></returns>
		[HttpGet("accounts")]
		public async Task<IActionResult> GetAccountLogs(
			[FromQuery] string userId,
			string userName,
			string email,
			int type,
			DateTime? dateFrom,
			DateTime? dateTo)
		{
			try
			{
				return Ok(await _accountLogService.GetAccountLogsAsync(userId:userId,
					userName:userName,
					email:email,
					type:type,
					dateFrom:dateFrom,
					dateTo:dateTo));
			}
			catch (SerializationException e)
			{
				return BadRequest($"{e.Message} : {e.Content}");
			}
		}

		/// <summary>
		/// Возвращает список логов по платежам
		/// </summary>
		/// <returns></returns>
		[HttpGet("payments")]
		public async Task<IActionResult> GetPaymentLogs(
			[FromQuery] string senderId,
			string recieverId,
			double? minTransfer,
			double? maxTransfer,
			DateTime? dateFrom,
			DateTime? dateTo)
		{
			try
			{
				return Ok(await _paymentLogService.GetPaymentLogsAsync(senderId:senderId,
					recieverId:recieverId,
					minTransfer:minTransfer,
					maxTransfer:maxTransfer,
					dateFrom: dateFrom,
					dateTo: dateTo));
			}
			catch (SerializationException e)
			{
				return BadRequest($"{e.Message} : {e.Content}");
			}
		}

		/// <summary>
		/// Возвращает список логов по поисковым запросам
		/// </summary>
		/// <returns></returns>
		[HttpGet("queries")]
		public async Task<IActionResult> GetSearchQueryLogs(
			[FromQuery] string userId,
			string searchCriterium,
			string filterInfo,
			DateTime? dateFrom,
			DateTime? dateTo)
		{
			try
			{
				return Ok(await _queryLogService.GetSearchQueryLogsAsync(userId: userId, searchCriterium: searchCriterium, filterInfo: filterInfo, dateFrom: dateFrom, dateTo: dateTo));
			}
			catch (SerializationException e)
			{
				return BadRequest($"{e.Message} : {e.Content}");
			}
		}

		/// <summary>
		/// Возвращает список логов по действиям с билетамми
		/// </summary>
		/// <returns></returns>
		[HttpGet("tickets")]
		public async Task<IActionResult> GetTicketActionLogs(
			[FromQuery] string ticketId,
			string ticketName,
			string description,
			int actionType,
			DateTime? dateFrom,
			DateTime? dateTo)
		{
			try
			{
				return Ok(await _ticketActionLogService.GetTicketActionLogsAsync(ticketId: ticketId,
					ticketName: ticketName,
					description: description,
					actionType:actionType,
					dateFrom: dateFrom,
					dateTo: dateTo));
			}
			catch (SerializationException e)
			{
				return BadRequest($"{e.Message} : {e.Content}");
			}
		}

		/// <summary>
		/// Возвращает список логов по сделкам по билетам
		/// </summary>
		/// <returns></returns>
		[HttpGet("deals")]
		public async Task<IActionResult> GetTicketDealLogs(
			[FromQuery] string ticketId,
			string ownerId,
			string recieverId,
			double? minPrice,
			double? maxPrice,
			int type,
			DateTime? dateFrom,
			DateTime? dateTo)
		{
			try
			{
				return Ok(await _ticketDealLogService.GetTicketDealLogsAsync(ticketId: ticketId,
					ownerId: ownerId,
					recieverId: recieverId,
					minPrice: minPrice,
					maxPrice: maxPrice,
					type: type,
					dateFrom: dateFrom,
					dateTo: dateTo));
			}
			catch (SerializationException e)
			{
				return BadRequest($"{e.Message} : {e.Content}");
			}
		}
	}
}