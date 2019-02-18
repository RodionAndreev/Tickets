using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Repositories
{
    public interface IProfileRepository : IRepository<ProfileDB>
    {
        Task<ProfileDB> UpdateProfileAsync(ProfileDB t, object key);
    }
}
