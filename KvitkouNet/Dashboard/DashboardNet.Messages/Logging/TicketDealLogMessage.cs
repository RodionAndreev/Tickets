using System;
using KvitkouNet.Messages.Logging.Enums;

namespace KvitkouNet.Messages.Logging
{
    /// <summary>
    /// Модель сообщения о сделке по билету
    /// </summary>
    public class TicketDealLogMessage
    {
        /// <summary>
        /// Дата логируемого события
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.Now;

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
        public DealType Type { get; set; }
    }
}
