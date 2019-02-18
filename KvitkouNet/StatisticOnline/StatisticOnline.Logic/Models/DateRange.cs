using System;

/// <summary>
/// Модель диапазона дат для запросов статистики
/// </summary>

namespace StatisticOnline.Logic.Models
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
