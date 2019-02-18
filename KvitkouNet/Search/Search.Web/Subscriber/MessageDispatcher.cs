using System;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using Microsoft.Extensions.DependencyInjection;

namespace Search.Web.Subscriber
{
    public class MessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly IServiceProvider _container;

        public MessageDispatcher(IServiceProvider container)
        {
            _container = container;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : class, IConsume<TMessage>
        {
            using (var scope = _container.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetService<TConsumer>();
                consumer.Consume(message);
            }
        }

        public async Task DispatchAsync<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : class, IConsumeAsync<TMessage>
        {
            using (var scope = _container.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetService<TConsumer>();
                await consumer.ConsumeAsync(message).ConfigureAwait(false);
            }
        }
    }
}
