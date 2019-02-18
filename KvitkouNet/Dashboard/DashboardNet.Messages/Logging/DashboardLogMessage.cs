using System;

namespace KvitkouNet.Messages.Logging
{
    /// <summary>
    /// Модель сообщения о действии с билетом
    /// </summary>
    public class DashboardLogMessage
    {
        /// <summary>
        /// Дата логируемой новости
        /// </summary>
        public DateTime EventDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Id новости
        /// </summary>
        public string NewsId { get; set; }

        /// <summary>
        /// Id пользователя, выполнившего действие с билетом
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Id билета
        /// </summary>
        public string TicketId { get; set; }
        
        /// <summary>
        /// Описание и дополнительное содержимое действия
        /// </summary>
        public string Description { get; set; }
    }
}