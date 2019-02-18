using System.Threading.Tasks;
using AutoMapper;
using Search.Data.Models;
using Search.Data.Repositories;
using Search.Logic.Common.Models;

namespace Search.Logic.Services
{
    public class SearchHistoryService : ISearchHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly IMapper _mapper;

        public SearchHistoryService(IHistoryRepository historyRepository, IMapper mapper)
        {
            _historyRepository = historyRepository;
            _mapper = mapper;
        }

        public Task<UserSearchEntity> GetLastUserSearchAsync(string userId)
        {
            return _historyRepository.GetLastUserSearch(userId);
        }

        public Task<TicketSearchEntity> GetLastTicketSearchAsync(string userId)
        {
            return _historyRepository.GetLastTicketSearch(userId);
        }

        public Task SaveLastSearchAsync(TicketSearchRequest request, string userId)
        {
            var searchEntity = _mapper.Map<TicketSearchEntity>(request);
            searchEntity.UserId = userId;
            return _historyRepository.SaveLastSearchAsync(searchEntity);
        }

        public Task SaveLastSearchAsync(UserSearchRequest request, string userId)
        {
            var searchEntity = _mapper.Map<UserSearchEntity>(request);
            searchEntity.UserId = userId;
            return _historyRepository.SaveLastSearchAsync(searchEntity);
        }

        public void Dispose()
        {
        }
    }
}
