using System.Collections.Generic;
using Security.Data.ContextModels;
using Security.Data.Models;

namespace Security.Data.ConfigModels
{
    public class DefaultRulesAll
    {
        public List<RoleDb> Roles { get; set; }

        public List<AccessFunctionDb> Functions { get; set; }

        public List<FeatureDb> Features { get; set; }

        public List<AccessRightDb> AccessRights { get; set; }
    }
}
