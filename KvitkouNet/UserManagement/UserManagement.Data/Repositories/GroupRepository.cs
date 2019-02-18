using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Data.Context;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Repositories
{
    class GroupRepository : BaseRepository<GroupDB>, IGroupRepository
    {
        public GroupRepository(UserContext db) : base(db) { }
    }
}
