using Logging.Logic.Models;
using Logging.Logic.Models.Filters;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс сервиса для работы с логами об ошибках
    /// </summary>
    public interface IErrorLogService : ILogService<InternalErrorLogEntry, ErrorLogsFilter>
    {
    }
}