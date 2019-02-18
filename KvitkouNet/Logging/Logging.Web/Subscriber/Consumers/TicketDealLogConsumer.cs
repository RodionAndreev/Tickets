using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;

namespace Logging.Web.Subscriber.Consumers
{
	/// <summary>
	/// Класс для получения сообщений о сделках по билетам из TicketManagement
	/// </summary>
	public class TicketDealLogConsumer : IConsumeAsync<TicketDealLogMessage>
	{
		private readonly IMapper _mapper;
		private readonly IDealLogService _dealLogService;

		public TicketDealLogConsumer(IMapper mapper, IDealLogService dealLogService)
		{
			_mapper = mapper;
			_dealLogService = dealLogService;
		}

		/// <summary>
		/// Метод для обработки сообщений о сделках по билетам
		/// </summary>
		/// <param name="message">Сообщение о сделке с билетом</param>
		/// <returns></returns>
		public async Task ConsumeAsync(TicketDealLogMessage message)
		{
			await _dealLogService.AddLogAsync(_mapper.Map<TicketDealLogEntry>(message));
		}
	}
}