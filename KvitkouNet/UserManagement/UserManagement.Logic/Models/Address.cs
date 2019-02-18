namespace UserManagement.Logic.Models
{
    public class Address
    {
        /// <summary>
        /// Страна проживания
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Номер дома, корпус
        /// </summary>
        public string House { get; set; }

        /// <summary>
        /// Номер квартиры
        /// </summary>
        public string Flat { get; set; }
    }
}