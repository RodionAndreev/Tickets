using System;

namespace StatisticUser.Logic.Interfaces
{
    /// <summary>
    /// Количество регистраций по времени
    /// </summary>
    public interface IRegistrationTime
    {
        DateTime Date { get; set; }
        int CountRegistrations { get; set; }
    }
}