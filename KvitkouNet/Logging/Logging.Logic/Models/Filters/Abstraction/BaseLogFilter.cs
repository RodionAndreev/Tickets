using System;

namespace Logging.Logic.Models.Filters.Abstraction
{
    /// <summary>
    /// Базовый класс для фильтров полчения логов
    /// </summary>
    public class BaseLogFilter
    {
        /// <summary>
        /// Дата, с которой получить логи
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// Дата, по которую получить логи
        /// </summary>
        public DateTime? DateTo { get; set; }
    }
}
