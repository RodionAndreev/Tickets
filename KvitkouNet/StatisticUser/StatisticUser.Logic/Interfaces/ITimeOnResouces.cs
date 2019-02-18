using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticUser.Logic.Interfaces
{
    /// <summary>
    /// время проведенное на ресуйсах сайта
    /// </summary>
    public interface ITimeOnResouces
    {
        string Resource { get; set; }
        DateTime DateTime { get; set; }
        TimeSpan TimeOnResource { get; set; }
    }
}
