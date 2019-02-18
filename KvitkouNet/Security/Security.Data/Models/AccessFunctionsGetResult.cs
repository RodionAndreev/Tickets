using System.Collections.Generic;

namespace Security.Data.Models
{
    public class AccessFunctionsGetResult
    {
        public IEnumerable<AccessFunctionDb> Functions { get; set; }

        public int TotalCount { get; set; }
    }
}
