using System;
using System.Collections.Generic;
using System.Linq;
using Security.Logic.Models;

namespace Security.Logic.Tests.Comparers
{
    public class RoleComparer : Comparer<Role>
    {
        public override int Compare(Role x, Role y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (ReferenceEquals(x, null))
                return -1;
            if (ReferenceEquals(y, null))
                return 1;

            
            if (x.Id == y.Id && x.Name == y.Name && Enumerable.SequenceEqual(x.AccessRights, y.AccessRights, new AccessRightComparer()) 
                && Enumerable.SequenceEqual(x.DeniedRights, y.DeniedRights, new AccessRightComparer())
                && Enumerable.SequenceEqual(x.AccessFunctions, y.AccessFunctions, new FunctionComparer()))
            {
                return 0;
            }

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
