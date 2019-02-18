using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Logic.Services;

namespace Security.Web.Subscriber
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
                    Component.For<IUserRightsService>().Instance(services.GetService<IUserRightsService>()),

                    //Потребители
                    Component.For<ConfirmRegistrationNotificationMessageConsumer>().ImplementedBy<ConfirmRegistrationNotificationMessageConsumer>(),
                    Component.For<CreationNotificationMessageConsumer>().ImplementedBy<CreationNotificationMessageConsumer>(),
                    Component.For<UpdateUserNotificationMessageConsumer>().ImplementedBy<UpdateUserNotificationMessageConsumer>(),
                    Component.For<DeleteUserNotificationMessageConsumer>().ImplementedBy<DeleteUserNotificationMessageConsumer>(),

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
