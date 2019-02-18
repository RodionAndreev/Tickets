using System;


namespace StatisticUser.Logic.DTOs
{
    /// <summary>
    /// Класс для запроса данных в диапазоне дат
    /// </summary>
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
