namespace TicketManagement.Logic.Models
{
    /// <summary>
    ///     Вспомогательный класс пользователя для тикета
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        ///     Id билета
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
        ///     Gets or sets the user rating.
        /// </summary>
        public double? Rating { get; set; }
    }
}