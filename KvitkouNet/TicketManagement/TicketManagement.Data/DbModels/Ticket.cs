using System;
using System.Collections.Generic;
using TicketManagement.Data.DbModels.DbEnums;

namespace TicketManagement.Data.DbModels
{
    /// <summary>
    ///     Класс описания модели билета
    /// </summary>
    public class Ticket
    {
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
        ///     Id билета
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        ///     Адрес проведения мероприятия
        /// </summary>
        public LocationAddress LocationEvent { get; set; }

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
        public SellerAddress SellerAdress { get; set; }

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
        public TypeEventTicketDb TypeEvent { get; set; }

        /// <summary>
        ///     Ссылка на мероприятие
        /// </summary>
        public string EventLink { get; set; }

        /// <summary>
        ///     Статус билета
        /// </summary>
        public TicketStatusDb Status { get; set; }

        /// <summary>
        ///     Дата создания билета
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}