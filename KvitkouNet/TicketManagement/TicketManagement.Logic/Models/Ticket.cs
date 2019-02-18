using System;
using System.Collections.Generic;

namespace TicketManagement.Logic.Models
{
    /// <summary>
    ///     Класс описания доменной модели билета
    /// </summary>
    public class Ticket
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
        ///     Пользователи, которые добавили билет в “Я Пойду”
        /// </summary>
        public List<UserInfo> RespondedUsers { get; set; }

        /// <summary>
        ///     Платный/бесплатный билет
        /// </summary>
        public bool Free { get; set; }

        /// <summary>
        ///     Название билета
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Адрес проведения мероприятия
        /// </summary>
        public Address LocationEvent { get; set; }

        /// <summary>
        ///     Цена билета
        /// </summary>
        public decimal? Price { get; set; }

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
        public DateTime TimeActual { get; set; }

        /// <summary>
        ///     Тип мероприятия
        /// </summary>
        public string TypeEvent { get; set; }

        /// <summary>
        ///     Ссылка на мероприятие
        /// </summary>
        public string EventLink { get; set; }

        /// <summary>
        ///     Статус билета
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///     Дата создания билета
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}