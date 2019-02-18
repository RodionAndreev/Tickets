using Logging.Logic.Models;
using Logging.Logic.Models.Filters;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс сервиса для работы с логами о действиях с билетами
    /// </summary>
    public interface ITicketLogService : ILogService<TicketActionLogEntry, TicketLogsFilter>
    {
    }
}