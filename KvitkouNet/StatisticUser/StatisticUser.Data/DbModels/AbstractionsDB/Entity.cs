using System;
using System.Collections.Generic;
using System.Text;

namespace StatisticUser.Data.DbModels.AbstractionsDB
{
    /// <summary>
    /// Базовый для таблиц
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey>
    {
        /// <summary>
        /// Id записи
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime Created { get; set; } = DateTime.Now;

        /// <summary>
        /// Дата изменения записи
        /// </summary>
        public DateTime? Modified { get; set; }
    }
}
