using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Data.DbModels
{
    public class UserGroupDB
    {
        public string UserId { get; set; }
        public virtual UserDB User { get; set; }
        public int GroupId { get; set; }
        public virtual GroupDB Group { get; set; }
    }
}
