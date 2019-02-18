using Logging.Data.DbModels.Abstraction;

namespace Logging.Data.DbModels
{
    /// <summary>
    /// Модель записи в лог о сделке по билету
    /// </summary>
    public class TicketDealLogEntryDbModel : BaseLogEntryDbModel
    {
        /// <summary>
        /// Id билета
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// Пользователь-владелец, разместивший билет
        /// </summary>
        public string OwnerId { get; set; }

        /// <summary>
        /// Покупатель/получатель билета
        /// </summary>
        public string RecieverId { get; set; }

        /// <summary>
        /// Цена билета, т.е. сумма сделки
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Тип сделки
        /// </summary>
        public int Type { get; set; }
    }
}
