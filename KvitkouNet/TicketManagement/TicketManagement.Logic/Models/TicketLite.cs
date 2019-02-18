using System;

namespace TicketManagement.Logic.Models
{
    /// <summary>
    ///     Класс описывающий минимальный набор необходимых свойств для вывода на странице
    /// </summary>
    public class TicketLite
    {
        /// <summary>
        ///     Id билета
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Пользователь разместивший билет
        /// </summary>
        public UserInfo User { get; set; }

        /// <summary>
        ///     Платный/бесплатный билет
        /// </summary>
        public bool Free { get; set; }

        /// <summary>
        ///     Название билета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Цена билета
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        ///     Статус билета
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Дата создания билета
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}