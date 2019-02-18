using System;
using System.Collections.Generic;
using System.Linq;
using Security.Logic.Models;

namespace Security.Logic.Tests.Comparers
{
    public class FeatureComparer : Comparer<Feature>
    {
        public override int Compare(Feature x, Feature y)
        {
            if (ReferenceEquals(x, y)) return 0;

            if (ReferenceEquals(x, null))
                return -1;
            if (ReferenceEquals(y, null))
                return 1;

            
            if (x.Id == y.Id && x.Name == y.Name && Enumerable.SequenceEqual(x.AvailableAccessRights, y.AvailableAccessRights, new AccessRightComparer()))
            {
                return 0;
            }

            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}
