using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Data.Context;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Repositories
{
    public class ProfileRepository : BaseRepository<ProfileDB>, IProfileRepository
    {
        public ProfileRepository(UserContext db) : base(db)
        {
        }

        public virtual async Task<ProfileDB> UpdateProfileAsync(ProfileDB t, object key)
        {
            if (t == null)
                return null;
            ProfileDB exist = await _context.Set<ProfileDB>().FindAsync(key);
            if (exist != null)
            {
                //_context.Entry(exist).CurrentValues.SetValues(t);
                exist.FirstName = t.FirstName;
                exist.LastName = t.LastName;
                exist.Sex = t.Sex;
                exist.Birthday = t.Birthday;
                await _context.SaveChangesAsync();
            }
            return exist;
        }
    }
}
