using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Search.Data.Models;
using Search.Data.Repositories;
using Search.Logic.Common.Models;

namespace Search.Logic.Services
{
    public class SearchTicketService : ISearchTicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISearchHistoryService _historyService;

        public SearchTicketService(ITicketRepository ticketRepository, ISearchHistoryService historyService)
        {
            _ticketRepository = ticketRepository;
            _historyService = historyService;
        }

        public async Task<SearchResult<TicketInfo>> Search(TicketSearchRequest request, string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                await _historyService.SaveLastSearchAsync(request, userId);
            }

            return await _ticketRepository.SearchAsync(request);
        }

        public void Dispose()
        {
        }

    }
}
