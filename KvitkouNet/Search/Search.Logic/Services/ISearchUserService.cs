using System;
using System.Threading.Tasks;
using Search.Logic.Common.Models;

namespace Search.Logic.Services
{
    public interface ISearchUserService: IDisposable
    {
        /// <summary>
        /// Searches the users according to the specified <paramref name="request"/>.
        /// </summary>
        Task<SearchResult<UserInfo>> Search(UserSearchRequest request, string userId);
    }
}
