using System.Threading.Tasks;
using AutoMapper;
using EasyNetQ.AutoSubscribe;
using KvitkouNet.Messages.TicketManagement;
using Search.Data.Repositories;
using Search.Logic.Common.Models;

namespace Search.Web.Subscriber
{
    public class TicketMessageConsumer : IConsumeAsync<TicketCreationMessage>, IConsumeAsync<TicketUpdatedMessage>, IConsumeAsync<TicketDeletedMessage>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;

        public TicketMessageConsumer(ITicketRepository ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        [AutoSubscriberConsumer(SubscriptionId = "TicketService.Created")]
        public async Task ConsumeAsync(TicketCreationMessage message)
        {
            await _ticketRepository.SaveAsync(_mapper.Map<TicketInfo>(message));
        }

        [AutoSubscriberConsumer(SubscriptionId = "TicketService.Updated")]
        public async Task ConsumeAsync(TicketUpdatedMessage message)
        {
            await _ticketRepository.SaveAsync(_mapper.Map<TicketInfo>(message));
        }

        [AutoSubscriberConsumer(SubscriptionId = "TicketService.Deleted")]
        public async Task ConsumeAsync(TicketDeletedMessage message)
        {
            await _ticketRepository.DeleteAsync(message.TicketId);
        }
    }
}
