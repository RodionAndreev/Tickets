using System;
using System.Collections.Generic;
using System.Linq;
using Security.Logic.Models;

namespace Security.Logic.Tests.Comparers
{
    public class FunctionComparer : Comparer<AccessFunction>, IEqualityComparer<AccessFunction>
    {
        public override int Compare(AccessFunction x, AccessFunction y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (ReferenceEquals(x, null))
                return -1;
            if (ReferenceEquals(y, null))
                return 1;

            
            if (x.Id == y.Id && x.Name == y.Name && x.FeatureId == y.FeatureId && Enumerable.SequenceEqual(x.AccessRights, y.AccessRights, new AccessRightComparer()))
            {
                return 0;
            }

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }

        public bool Equals(AccessFunction x, AccessFunction y)
        {
            return x.Id == y.Id && x.Name == y.Name && x.FeatureId == y.FeatureId;
        }

        public int GetHashCode(AccessFunction obj)
        {
            throw new NotImplementedException();
        }
    }
}
