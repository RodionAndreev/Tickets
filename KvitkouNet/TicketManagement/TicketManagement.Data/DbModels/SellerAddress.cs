using System.Collections.Generic;

namespace TicketManagement.Data.DbModels
{
    /// <summary>
    ///     Класс описывающий адресс нахождения продавца
    /// </summary>
    public class SellerAddress
    {
        /// <summary>
        ///     Id адреса
        /// </summary>
        public string SellerAddressId { get; set; }

        /// <summary>
        ///     Страна проживания
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///     Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///     Номер дома, корпус
        /// </summary>
        public string House { get; set; }

        /// <summary>
        ///     Номер квартиры
        /// </summary>
        public string Flat { get; set; }

        /// <summary>
        ///     Список билетов по этому адресу
        /// </summary>
        public List<Ticket> Tickets { get; set; }
    }
}