using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Repositories
{
    public class UserRepository : BaseRepository<UserDB>, IUserRepository
    {
        public UserRepository(UserContext db) : base(db)
        {
        }

        public virtual async Task<UserDB> GetByLoginAsync(string login)
        {
            return await _context.Set<UserDB>().FirstOrDefaultAsync(u => u.AccountDB.Login == login);
        }
    }
}
