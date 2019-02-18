using System;

namespace Logging.Data.DbModels.Abstraction
{
    /// <summary>
    /// Базовый класс для всех бд-моделей записей лога
    /// </summary>
    public abstract class BaseLogEntryDbModel
    {
        /// <summary>
        /// Id записи
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Дата логируемого события
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
