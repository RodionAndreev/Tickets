using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Logic.Infrastructure;
using Logging.Logic.Models;

namespace Logging.Web.Subscriber.Consumers
{
	/// <summary>
	/// Класс для получения сообщений о платежах из микросервиса Payment
	/// </summary>
	public class PaymentLogConsumer : IConsumeAsync<PaymentLogMessage>
	{
		private readonly IMapper _mapper;
		private readonly IPaymentLogService _paymentLogService;

		public PaymentLogConsumer(IMapper mapper, IPaymentLogService paymentLogService)
		{
			_mapper = mapper;
			_paymentLogService = paymentLogService;
		}

		/// <summary>
		/// Метод для обработки сообщений о платежах
		/// </summary>
		/// <param name="message">Сообщение о платеже</param>
		/// <returns></returns>
		public async Task ConsumeAsync(PaymentLogMessage message)
		{
			await _paymentLogService.AddLogAsync(_mapper.Map<PaymentLogEntry>(message));
		}
	}
}