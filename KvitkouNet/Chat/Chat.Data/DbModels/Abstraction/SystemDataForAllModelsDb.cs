using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Data.DbModels.Abstraction
{
    /// <summary>
    /// Базовый класс содержащий системную информацию для всех моделей БД
    /// </summary>
    /// <typeparam name="TKey">Тип свойства Id для конкретной модели</typeparam>
    public abstract class SystemDataForAllModelsDb<TKey>
    {
        /// <summary>
        /// Id записи
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}
