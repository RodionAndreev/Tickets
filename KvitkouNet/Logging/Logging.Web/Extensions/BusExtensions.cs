using System;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Web.Extensions
{
	/// <summary>
	/// Класс методов расширения для IBus
	/// </summary>
	public static class BusExtensions
	{
		/// <summary>
		/// Подписка всех требуемых IConsumer
		/// </summary>
		/// <param name="bus"></param>
		/// <param name="services"></param>
		/// <returns></returns>
		public static IBus SubscribeAllConsumers(this IBus bus, IServiceProvider services)
		{
			var accountLogPrefix = "AccountLogging.Added";
			var errorLogPrefix = "ErrorLogging.Added";
			var paymentLogPrefix = "PaymentLogging.Added";
			var searchLogPrefix = "SearchLogging.Added";
			var ticketActionLogPrefix = "TicketActionLogging.Added";
			var ticketDealLogPrefix = "TicketDealLogging.Added";
			bus.SubscribeOnMessage<AccountLogMessage>(accountLogPrefix, services);
			bus.SubscribeOnMessage<InternalErrorLogMessage>(errorLogPrefix, services);
			bus.SubscribeOnMessage<PaymentLogMessage>(paymentLogPrefix, services);
			bus.SubscribeOnMessage<SearchQueryLogMessage>(searchLogPrefix, services);
			bus.SubscribeOnMessage<TicketActionLogMessage>(ticketActionLogPrefix, services);
			bus.SubscribeOnMessage<TicketDealLogMessage>(ticketDealLogPrefix, services);
			return bus;
		}

		private static IBus SubscribeOnMessage<TMessage>(this IBus bus, string prefix, IServiceProvider services) where TMessage:class
		{
			bus.SubscribeAsync<TMessage>(prefix, msg => services.GetService<IConsumeAsync<TMessage>>().ConsumeAsync(msg));
			return bus;
		}
	}
}