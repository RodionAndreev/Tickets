using System.Reflection;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Search.Web.Subscriber
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
                var subscriber = new AutoSubscriber(bus, prefix)
                {
                    AutoSubscriberMessageDispatcher = new MessageDispatcher(app.ApplicationServices)
                };
                subscriber.Subscribe(assembly);
                subscriber.SubscribeAsync(assembly);
            });

            lifetime.ApplicationStopped.Register(() => bus.Dispose());

            return app;
        }

        public static IServiceCollection RegisterConsumers(this IServiceCollection services)
        {
            services.AddScoped<TicketMessageConsumer>();
            services.AddScoped<UserMessageConsumer>();

            return services;
        }
        
        internal static Task AwaitConnection(this IBus bus)
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
