using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;

namespace Logging.Web.Subscriber.Consumers
{
	/// <summary>
	/// Класс для получения сообщений о действиях с аккаунтом из микросервиса UserManagement
	/// </summary>
	public class AccountLogConsumer : IConsumeAsync<AccountLogMessage>
	{
		private readonly IMapper _mapper;
		private readonly IAccountLogService _accountLogService;

		public AccountLogConsumer(IMapper mapper, IAccountLogService accountLogService)
		{
			_mapper = mapper;
			_accountLogService = accountLogService;
		}

		/// <summary>
		/// Метод для обработки действий с аккаунтом
		/// </summary>
		/// <param name="message">Сообщение о действии с аккаунтом</param>
		/// <returns></returns>
		public async Task ConsumeAsync(AccountLogMessage message)
		{
			await _accountLogService.AddLogAsync(_mapper.Map<AccountLogEntry>(message));
		}
	}
}