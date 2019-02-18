using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Repositories
{
    public interface IUserRepository : IRepository<UserDB>
    {
        Task<UserDB> GetByLoginAsync(string login);
    }
}
