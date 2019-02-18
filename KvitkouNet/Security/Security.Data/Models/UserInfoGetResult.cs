using System.Collections.Generic;

namespace Security.Data.Models
{
    public class UserInfoGetResult
    {
        public IEnumerable<UserInfoDb> UsersInfo { get; set; }

        public int TotalCount { get; set; }
    }
}
