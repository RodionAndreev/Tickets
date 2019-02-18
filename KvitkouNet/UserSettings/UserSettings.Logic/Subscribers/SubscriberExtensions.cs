using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.UserManagement;
using Microsoft.Extensions.DependencyInjection;
using System;
using Polly;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace UserSettings.Logic
{
	public static class SubscriberExtensions
	{

		public static IBus SubscribeAll(this IBus bus, IServiceProvider services)
		{
			bus.SubscribeAsync<UserCreationMessage>("GetUserProfile.Added", msg => services.GetService<IConsumeAsync<UserCreationMessage>>().ConsumeAsync(msg));
			return bus;
		}
		public static IApplicationBuilder UseSubscriber(this IApplicationBuilder app)
		{
			string UserCreationPrefix = "UserCreation.Created";
			var services = app.ApplicationServices.CreateScope().ServiceProvider;

			var policy = Policy.Handle<TimeoutException>().WaitAndRetryAsync(new[]
			{
				TimeSpan.FromSeconds(1)
			});

			var lifetime = services.GetService<IApplicationLifetime>();
			var bus = services.GetService<IBus>();

			lifetime.ApplicationStarted.Register(() =>
			{
				try
				{
						bus.SubscribeAsync<UserCreationMessage>(UserCreationPrefix,
								msg => services.GetService<IConsumeAsync<UserCreationMessage>>().ConsumeAsync(msg));
				}
				catch
				{

				}
			});

			lifetime.ApplicationStopped.Register(() => bus.Dispose());

			return app;
		}
	}
}
