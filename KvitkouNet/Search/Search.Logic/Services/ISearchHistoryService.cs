using System;
using System.Threading.Tasks;
using Search.Data.Models;
using Search.Logic.Common.Models;

namespace Search.Logic.Services
{
    public interface ISearchHistoryService : IDisposable
    {
        /// <summary>
        /// Gets the last user search by <see cref="userId"/>.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task<UserSearchEntity> GetLastUserSearchAsync(string userId);

        /// <summary>
        /// Gets the last ticket search by <see cref="userId"/>.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        Task<TicketSearchEntity> GetLastTicketSearchAsync(string userId);

        Task SaveLastSearchAsync(TicketSearchRequest request, string userId);

        Task SaveLastSearchAsync(UserSearchRequest request, string userId);
    }
}
