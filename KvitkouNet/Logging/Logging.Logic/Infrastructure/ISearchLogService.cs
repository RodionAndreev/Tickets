using Logging.Logic.Models;
using Logging.Logic.Models.Filters;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс сервиса для работы с логами поисковых запросов
    /// </summary>
    public interface ISearchLogService : ILogService<SearchQueryLogEntry, SearchQueryLogsFilter>
    {
    }
}