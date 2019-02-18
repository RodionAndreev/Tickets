using Logging.Logic.Models;
using Logging.Logic.Models.Filters;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс сервиса для работы с логами о сделках с билетами
    /// </summary>
    public interface IDealLogService : ILogService<TicketDealLogEntry, DealLogFilter>
    {
    }
}