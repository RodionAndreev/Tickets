using System.Collections.Generic;

namespace TicketManagement.Data.DbModels
{
    /// <summary>
    ///     Вспомогательный класс пользователя для тикета
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        ///     Id юзера
        /// </summary>
        public string UserInfoId { get; set; }

        /// <summary>
        ///     Имя юзера
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Фамилия юзера
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Список билетов пользователя
        /// </summary>
        public List<Ticket> UserTickets { get; set; }
    }
}