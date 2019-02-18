using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Search.Logic.Common.Models;

namespace Search.Data.Repositories
{
    public interface IUserRepository : IRepository<UserInfo>
    {
        Task<SearchResult<UserInfo>> SearchAsync(UserSearchRequest request);
    }
}
