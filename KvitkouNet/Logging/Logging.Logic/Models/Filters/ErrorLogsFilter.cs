using System;
using Logging.Logic.Models.Filters.Abstraction;

namespace Logging.Logic.Models.Filters
{
    /// <summary>
    /// Фильтр для получения логов об ошибках
    /// </summary>
	public class ErrorLogsFilter : BaseLogFilter
    {
        /// <summary>
        /// Название микросервиса, передавшего сообщение об ошибке
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Название типа исключения
        /// </summary>
		public string ExceptionTypeName { get; set; }

        /// <summary>
        /// Сообщение, описывающее исключение
        /// </summary>
        public string Message { get; set; }
    }
}
