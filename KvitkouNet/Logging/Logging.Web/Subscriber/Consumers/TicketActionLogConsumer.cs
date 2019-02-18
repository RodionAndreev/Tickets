using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;

namespace Logging.Web.Subscriber.Consumers
{
	/// <summary>
	/// Класс для получения сообщений о действий с билетами из TicketManagement
	/// </summary>
	public class TicketActionLogConsumer : IConsumeAsync<TicketActionLogMessage>
	{
		private readonly IMapper _mapper;
		private readonly ITicketLogService _ticketLogService;

		public TicketActionLogConsumer(IMapper mapper, ITicketLogService ticketLogService)
		{
			_mapper = mapper;
			_ticketLogService = ticketLogService;
		}

		/// <summary>
		/// Метод для обработки сообщений о действиях с билетом
		/// </summary>
		/// <param name="message">Сообщение о действии с билетом</param>
		/// <returns></returns>
		public async Task ConsumeAsync(TicketActionLogMessage message)
		{
			await _ticketLogService.AddLogAsync(_mapper.Map<TicketActionLogEntry>(message));
		}
	}
}