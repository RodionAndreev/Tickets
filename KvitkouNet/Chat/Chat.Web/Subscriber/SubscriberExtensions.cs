
using System.Reflection;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Chat.Logic.Services;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.Web.Subscriber
{
    public static class SubscriberExtensions
    {
        public static IApplicationBuilder UseSubscriber(this IApplicationBuilder app, string prefix, Assembly assembly)
        {
            var services = app.ApplicationServices.CreateScope().ServiceProvider;

            var lifetime = services.GetService<IApplicationLifetime>();
            var bus = services.GetService<IBus>();

            //register our consumer with our IoC container
            var container = new WindsorContainer();
            container.Register(
                //регистраци потребителей
                Component.For<UserMessageConsumer>().ImplementedBy<UserMessageConsumer>(),

                //сервисы
                Component.For<IChatService>().Instance(services.GetService<IChatService>()),

                //мапперы
                Component.For<IMapper>().Instance(services.GetService<IMapper>())
                );

            lifetime.ApplicationStarted.Register(() =>
            {
                var subscriber = new AutoSubscriber(bus, prefix)
                {
                    AutoSubscriberMessageDispatcher = new WindsorMessageDispatcher(container)
                };
                subscriber.Subscribe(assembly);
                subscriber.SubscribeAsync(assembly);
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                container.Dispose();
                bus.Dispose();
            });

            return app;
        }
    }
}
