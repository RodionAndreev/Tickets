using System;

namespace StatisticUser.Logic.Interfaces
{
    public interface IUserOnline
    {
        int Id { get; set; }
        TimeSpan TimeOnline { get; set; }
    }
}