using Logging.Data.DbModels.Abstraction;

namespace Logging.Data.DbModels
{
    /// <summary>
    /// Запись в лог ошибок в работе сервиса
    /// </summary>
    public class InternalErrorLogEntryDbModel : BaseLogEntryDbModel
    {
        /// <summary>
        /// Название микросервиса, передавшего сообщение об ошибке
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Название типа исключения
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Числовое значение, ассоциированное с типом исключения
        /// </summary>
        public int HResult { get; set; }

        /// <summary>
        /// Строковое представление вложенных исключений
        /// </summary>
        public string InnerExceptionString { get; set; }

        /// <summary>
        /// Сообщение, описывающее исключение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Источник исключения
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Трассировка стека вызовов
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Имя метода, вызвавшего исключение
        /// </summary>
        public string TargetSiteName { get; set; }
    }
}
