using System.Collections.Generic;

namespace Security.Data.Models
{
    public class FeaturesGetResult
    {
        public IEnumerable<FeatureDb> Features { get; set; }

        public int TotalCount { get; set; }
    }
}
