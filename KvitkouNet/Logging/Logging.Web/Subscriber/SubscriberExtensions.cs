using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using Logging.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Logging.Web.Subscriber
{
	public static class SubscriberExtensions
	{
		public static IApplicationBuilder UseSubscriber(this IApplicationBuilder app, string prefix,
			params Assembly[] assembly)
		{
			var services = app.ApplicationServices.CreateScope().ServiceProvider;

			var lifetime = services.GetService<IApplicationLifetime>();
			var bus = services.GetService<IBus>();

			lifetime.ApplicationStarted.Register(async () =>
			{
				await bus.AwaitConnection().ConfigureAwait(false);
				bus.SubscribeAllConsumers(services);
			});

			lifetime.ApplicationStopped.Register(() => bus.Dispose());

			return app;
		}

		private static Task AwaitConnection(this IBus bus)
		{
			if (bus.IsConnected)
				return Task.CompletedTask;

			var tcs = new TaskCompletionSource<object>();
			bus.Advanced.Connected += (s, e) => tcs.TrySetResult(null);
			if (bus.IsConnected)
			{
				tcs.TrySetResult(null);
			}

			return tcs.Task;
		}
	}
}