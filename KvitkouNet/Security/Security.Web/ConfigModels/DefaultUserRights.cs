using System.Collections.Generic;
using Security.Logic.Models;

namespace Security.Web.ConfigModels
{
    public class DefaultUserRights
    {
        public List<Role> Roles { get; set; }

        public List<AccessFunction> Functions { get; set; }

        public List<AccessRight> AccessRights { get; set; }

        public List<AccessRight> DeniedRights { get; set; }
    }
}
