using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Search.Data.Context;
using Search.Data.Models;

namespace Search.Data.Repositories
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly SearchContext _context;

        public HistoryRepository(SearchContext context)
        {
            _context = context;
        }

        public Task<UserSearchEntity> GetLastUserSearch(string userId)
        {
            return _context.UserSearchEntities
                .OrderByDescending(x => x.SearchTime)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<TicketSearchEntity> GetLastTicketSearch(string userId)
        {
            return _context.TicketSearchEntities
                .OrderByDescending(x => x.SearchTime)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task SaveLastSearchAsync(SearchEntity entity)
        {
            await _context.SearchEntities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
