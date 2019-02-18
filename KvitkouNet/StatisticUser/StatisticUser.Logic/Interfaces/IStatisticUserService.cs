using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;
using StatisticUser.Logic.DTOs;
using StatisticUser.Logic.Services;

namespace StatisticUser.Logic.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса для работы с StatisticUser
    /// </summary>
    public interface IStatisticUserService: IDisposable
    {
        /// <summary>
        /// Возвращает списоко ресурсов сайта и сумарного
        /// времений проведенного на них 
        /// </summary>
        /// <param name="filter">Диапазон дат</param>
        /// <returns></returns>
        Task<IEnumerable<ITimeOnResouces>> GetTimeOnResouces(DateRange filter);

        /// <summary>
        /// Время проведенное пользователем на сайте
        /// в текущем сеансе
        /// </summary>
        /// <param name="id">ID пользователя</param>
        /// <returns></returns>
        Task<IUserOnline> GetUserOnline(int id);

        /// <summary>
        /// Рейтинг пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IUserRating> GetUserRating(int id);

        /// <summary>
        /// Временной график с регистрациями новых пользователей
        /// </summary>
        /// <param name="filter">Диапазон дат</param>
        /// <returns></returns>
        Task<IEnumerable<IRegistrationTime>> GetRegistrationsTime(DateRange filter);

        /// <summary>
        /// Количество сообщений пользователя
        /// </summary>
        Task<IUserMessages> GetUserMessages(int id);

        /// <summary>
        /// Количество сообщений пользователя
        /// </summary>
        LoadResult GetAllUser(DataSourceLoadOptions loadOptions);
    }
}
