using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Data.Context;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Repositories
{
    public class AccountRepository : BaseRepository<AccountDB>, IAccountRepository
    {
        public AccountRepository(UserContext db) : base(db) { }
    }
}
