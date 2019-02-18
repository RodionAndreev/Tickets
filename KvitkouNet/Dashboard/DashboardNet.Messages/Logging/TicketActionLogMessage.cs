using System;
using KvitkouNet.Messages.Logging.Enums;

namespace KvitkouNet.Messages.Logging
{
    /// <summary>
    /// Модель сообщения о действии с билетом
    /// </summary>
    public class TicketActionLogMessage
    {
        /// <summary>
        /// Дата логируемого события
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Id пользователя, выполнившего действие с билетом
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id билета
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        /// Тип действия с билетом
        /// </summary>
        public TicketAction ActionType { get; set; }

        /// <summary>
        /// Описание и дополнительное содержимое действия
        /// </summary>
        public string Description { get; set; }
    }
}