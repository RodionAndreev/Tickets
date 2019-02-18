using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;

namespace Logging.Web.Subscriber.Consumers
{
	/// <summary>
	/// Класс для обработки сообщений об ошибках с микросервисов
	/// </summary>
	public class InternalErrorLogConsumer : IConsumeAsync<InternalErrorLogMessage>
	{
		private readonly IMapper _mapper;
		private readonly IErrorLogService _errorLogService;

		public InternalErrorLogConsumer(IMapper mapper, IErrorLogService errorLogService)
		{
			_mapper = mapper;
			_errorLogService = errorLogService;
		}

		/// <summary>
		/// Метод для обработки сообщений об ошибке
		/// </summary>
		/// <param name="message">Сообщение об ошибке</param>
		/// <returns></returns>
		public async Task ConsumeAsync(InternalErrorLogMessage message)
		{
			await _errorLogService.AddLogAsync(_mapper.Map<InternalErrorLogEntry>(message));
		}
	}
}