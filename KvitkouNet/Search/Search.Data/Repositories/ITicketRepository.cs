using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Search.Logic.Common.Models;

namespace Search.Data.Repositories
{
    public interface ITicketRepository : IRepository<TicketInfo>
    {
        Task<SearchResult<TicketInfo>> SearchAsync(TicketSearchRequest request);
    }
}
