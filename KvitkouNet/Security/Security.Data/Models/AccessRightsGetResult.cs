using System.Collections.Generic;

namespace Security.Data.Models
{
    public class AccessRightsGetResult
    {
        public IEnumerable<AccessRightDb> Rights { get; set; }

        public int TotalCount { get; set; }
    }
}
