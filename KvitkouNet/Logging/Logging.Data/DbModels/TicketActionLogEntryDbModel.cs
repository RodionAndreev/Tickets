using Logging.Data.DbModels.Abstraction;

namespace Logging.Data.DbModels
{
    /// <summary>
    /// Модель записи в лог о действии с билетом
    /// </summary>
    public class TicketActionLogEntryDbModel : BaseLogEntryDbModel
    {
        /// <summary>
        /// Id пользователя, выполнившего действие с билетом
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id билета
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// Название билета
        /// </summary>
        public string TicketName { get; set; }

        /// <summary>
        /// Тип действия с билетом
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// Описание и дополнительное содержимое действия
        /// </summary>
        public string Description { get; set; }
    }
}
