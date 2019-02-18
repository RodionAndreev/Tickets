using System;
using System.Threading.Tasks;
using Search.Logic.Common.Models;

namespace Search.Logic.Services
{
    public interface ISearchTicketService : IDisposable
    {
        /// <summary>
        /// Searches the tickets according to the specified <paramref name="request"/>.
        /// </summary>
        Task<SearchResult<TicketInfo>> Search(TicketSearchRequest request, string userId);
    }
}
