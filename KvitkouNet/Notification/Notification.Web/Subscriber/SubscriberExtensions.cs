using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Notification.Logic.Services;
using Notification.Logic.Services.Interfaces;

namespace Notification.Web.Subscriber
{
	public static class SubscriberExtensions
	{
		public static IApplicationBuilder UseSubscriber(this IApplicationBuilder app, string prefix, 
			params Assembly[] assembly) 
		{
            var services = app.ApplicationServices.CreateScope().ServiceProvider;

            var lifetime = services.GetService<IApplicationLifetime>();
            var bus = services.GetService<IBus>();

            var container = new WindsorContainer();
            container.Register(     
                    //Маппер
                    Component.For<IMapper>().Instance(services.GetService<IMapper>()),

                    //Сервисы
                    Component.For<INotificationService>().Instance(services.GetService<INotificationService>()),
                    Component.For<IEmailNotificationService>().Instance(services.GetService<IEmailNotificationService>()),
                    Component.For<ISubscriptionService>().Instance(services.GetService<ISubscriptionService>()),
                    Component.For<IUserService>().Instance(services.GetService<IUserService>()),

                    //Потребители
                    Component.For<RegistrationNotificationMessageConsumer>().ImplementedBy<RegistrationNotificationMessageConsumer>(),
                    Component.For<SubscribersNotificationMessageConsumer>().ImplementedBy<SubscribersNotificationMessageConsumer>(),
                    Component.For<UserNotificationMessageConsumer>().ImplementedBy<UserNotificationMessageConsumer>(),
                    Component.For<UserSubscriptionMessageConsumer>().ImplementedBy<UserSubscriptionMessageConsumer>(),
                    Component.For<UserUnsubscriptionMessageConsumer>().ImplementedBy<UserUnsubscriptionMessageConsumer>(),
                                       
                    //Конфиг
                    Component.For<IConfiguration>().Instance(services.GetService<IConfiguration>()));

            lifetime.ApplicationStarted.Register(() =>
            {
                var subscriber = new AutoSubscriber(bus, prefix)
                {
                    AutoSubscriberMessageDispatcher = new WindsorMessageDispatcher(container)
                };
                subscriber.Subscribe(assembly);
                subscriber.SubscribeAsync(assembly);
            });

            lifetime.ApplicationStopped.Register(() => bus.Dispose());

            return app;
		}
	}
}