using System.Threading.Tasks;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;

namespace Notification.Web.Subscriber
{
	public class WindsorMessageDispatcher : IAutoSubscriberMessageDispatcher
	{
		private readonly IWindsorContainer m_container;

		public WindsorMessageDispatcher(IWindsorContainer container)
		{
			m_container = container;
		}

		public void Dispatch<TMessage, TConsumer>(TMessage message) where TMessage : class where TConsumer : class, IConsume<TMessage>
		{
			var consumer = m_container.Resolve<TConsumer>();
			try
			{
				consumer.Consume(message);
			}
			finally
			{
				m_container.Release(consumer);
			}
		}

		public Task DispatchAsync<TMessage, TConsumer>(TMessage message) where TMessage : class where TConsumer : class, IConsumeAsync<TMessage>
		{
			var consumer = m_container.Resolve<TConsumer>();
			return consumer.ConsumeAsync(message).ContinueWith(t => m_container.Release(consumer));
		}
	}
}
