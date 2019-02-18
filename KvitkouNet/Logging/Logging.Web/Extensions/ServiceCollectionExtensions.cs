using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.Logging;
using Logging.Web.Subscriber.Consumers;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Web.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection RegisterConsumers(this IServiceCollection services)
		{
			services.AddScoped<IConsumeAsync<AccountLogMessage>, AccountLogConsumer>();
			services.AddScoped<IConsumeAsync<InternalErrorLogMessage>, InternalErrorLogConsumer>();
			services.AddScoped<IConsumeAsync<PaymentLogMessage>, PaymentLogConsumer>();
			services.AddScoped<IConsumeAsync<SearchQueryLogMessage>, SearchQueryLogConsumer>();
			services.AddScoped<IConsumeAsync<TicketActionLogMessage>, TicketActionLogConsumer>();
			services.AddScoped<IConsumeAsync<TicketDealLogMessage>, TicketDealLogConsumer>();
			return services;
		}
	}
}