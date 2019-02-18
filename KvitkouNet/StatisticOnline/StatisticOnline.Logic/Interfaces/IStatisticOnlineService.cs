using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatisticOnline.Data.Models;
using StatisticOnline.Logic.Models;

namespace StatisticOnline.Logic.Interfaces
{
    public interface IStatisticOnlineService
    {
        /// <summary>
        /// число всех пользователей на сайте Online
        /// возвращает последнюю запись из базы
        /// </summary>
        Task<OnlineModel> GetAllUsers();

        /// <summary>
        /// число пользователей на сайте Online
        /// в диапазоне дат
        /// </summary>
        Task<IEnumerable<OnlineModel>> GetDateRangeUsers(DateRange range);

        /// <summary>
        /// число зарегистрированных пользователей
        /// возвращает последнюю запись из базы
        /// </summary>
        Task<int> GetRegisteredUser();

        /// <summary>
        /// число гостей на сайте Online
        /// возвращает последнюю запись из базы
        /// </summary>
        Task<int> GetGuestUser();
    }
}
