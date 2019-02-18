using System;
namespace Security.Data.Exceptions
{
    /// <summary>
    /// Ошибка выполнения операции
    /// </summary>
    public class SecurityDbException : Exception
    {
        public SecurityDbException(string message, ExceptionType code, EntityType entityType, string[] items) : base(message)
        {
            ExceptionType = code;
            EntityType = entityType;
            Items = items;
        }

        /// <summary>
        /// Тип ошибки
        /// </summary>
        public ExceptionType ExceptionType { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public EntityType EntityType { get; set; }

        /// <summary>
        /// Значения не прошедшие проверку
        /// </summary>
        public string[] Items { get; set; }
    }
}
