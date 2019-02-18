using Logging.Logic.Models;
using Logging.Logic.Models.Filters;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс сервиса для работы с логами аккаунтов
    /// </summary>
    public interface IAccountLogService : ILogService<AccountLogEntry, AccountLogsFilter>
    {
    }
}