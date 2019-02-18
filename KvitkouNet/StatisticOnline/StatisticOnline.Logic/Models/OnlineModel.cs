using System;
using System.Collections.Generic;
using System.Linq;


namespace StatisticOnline.Logic.Models
{
    /// <summary>
    /// Статистика состояния пользователей на сайте которые Online 
    /// </summary>
    public class OnlineModel
    {
        public int Id { get; set; }

        /// <summary>
        /// число всех пользователей на сайте Online
        /// </summary>
        public int CountAll { get; set; }

        /// <summary>
        /// число зарегистрированных пользователей
        /// </summary>
        public int CountRegistered { get; set; }

        /// <summary>
        /// число гостей на сайте Online
        /// </summary>
        public int CountGuest { get; set; }

        /// <summary>
        /// Дата и время на которую сформирована записи
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}