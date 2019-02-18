using System.Collections.Generic;

namespace Security.Data.Models
{
    public class RolesGetResult
    {
        public IEnumerable<RoleDb> Roles { get; set; }

        public int TotalCount { get; set; }
    }
}
