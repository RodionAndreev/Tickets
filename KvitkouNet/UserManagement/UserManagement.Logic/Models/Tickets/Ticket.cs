
using System.Collections.Generic;
using UserManagement.Logic.Models.Tickets.Enums;

namespace UserManagement.Logic.Models.Tickets
{
    /// <summary>
    ///     Класс описания доменной модели билета
    /// </summary>
    public class Ticket
    {
        /// <summary>
        ///     Пользователь разместивший билет
        /// </summary>
        public User User { get; set; }

        /// <summary>
        ///     Пользователи, которые добавили билет в “Я Пойду”
        /// </summary>
        public List<User> RespondedUsers { get; set; }

        /// <summary>
        ///     Платный/бесплатный билет
        /// </summary>
        public bool Free { get; set; }

        /// <summary>
        ///     Название билета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Id билета
        /// </summary>
        public string TicketId { get; set; }

        /// <summary>
        ///     Адрес проведения мероприятия
        /// </summary>
        public Address LocationEvent { get; set; }

        /// <summary>
        ///     Цена билета
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     Дополнительная информация билета
        /// </summary>
        public string AdditionalData { get; set; }

        /// <summary>
        ///     Телефон продавца
        /// </summary>
        public string SellerPhone { get; set; }

        /// <summary>
        ///     Адрес продавца
        /// </summary>
        public Address SellerAdress { get; set; }

        /// <summary>
        ///     Платежная система
        /// </summary>
        public string PaymentSystems { get; set; }

        /// <summary>
        ///     Время актуальности билета
        /// </summary>
        public string TimeActual { get; set; }

        /// <summary>
        ///     Тип мероприятия
        /// </summary>
        public TypeEventTicket TypeEvent { get; set; }

        /// <summary>
        ///     Ссылка на мероприятие
        /// </summary>
        public string EventLink { get; set; }

        /// <summary>
        ///     Статус билета
        /// </summary>
        public TicketStatus Status { get; set; }
    }
}
