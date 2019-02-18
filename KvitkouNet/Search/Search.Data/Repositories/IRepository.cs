using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Search.Logic.Common.Models;

namespace Search.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task SaveAsync(T item);

        Task DeleteAsync(string id);
    }
}
