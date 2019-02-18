using Logging.Logic.Models;
using Logging.Logic.Models.Filters;

namespace Logging.Logic.Infrastructure
{
    /// <summary>
    /// Интерфейс сервиса для работы с логами о переводах денег
    /// </summary>
    public interface IPaymentLogService : ILogService<PaymentLogEntry, PaymentLogsFilter>
    {
    }
}