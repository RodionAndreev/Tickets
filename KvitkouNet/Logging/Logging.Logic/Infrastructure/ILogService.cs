using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Logging.Logic.Models.Abstraction;
using Logging.Logic.Models.Filters.Abstraction;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс базового сервиса для работы с логами
    /// </summary>
    public interface ILogService<TEntry, in TFilter> : IDisposable where TEntry : BaseLogEntry where TFilter : BaseLogFilter
    {
        /// <summary>
        /// Получает логи по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для получения логов</param>
        /// <returns></returns>
        Task<IEnumerable<TEntry>> GetLogsAsync(TFilter filter);

        /// <summary>
        /// Записывает в лог
        /// </summary>
        /// <param name="entry">Модель записи</param>
        /// <returns></returns>
        Task AddLogAsync(TEntry entry);
    }
}