using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Dashboard.Logic.Models;
using Dashboard.Logic.Models.Enums;


namespace Dashboard.Logic.Services
{
    /// <summary>
    ///     Сервис для работы с Новостями
    /// </summary>
    public interface IDashboardService : IDisposable
    {
        /// <summary>
        ///     Добавляет новость
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        Task<(string, RequestStatus)> Add(News news);

        /// <summary>
        ///     Удаление всех новостей
        /// </summary>
        /// <returns></returns>
        Task<RequestStatus> DeleteAll();

        /// <summary>
        ///     Удаление новоти с определенным Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<RequestStatus> Delete(string newsId);

        /// <summary>
        ///     Получение всех новостей имеющихся в системе
        /// </summary>
        /// <returns></returns>
        Task<(IEnumerable<News>, RequestStatus)> GetAll();

        /// <summary>
        ///     Получение новости по Id
        /// </summary>
        /// <param name="newsId">Id билета</param>
        /// <returns></returns>
        Task<(News, RequestStatus)> Get(string newsId);

        /// <summary>
        ///     Автоматическое добовление новости
        /// </summary>
        /// <returns></returns>
        Task<RequestStatus> AddAutoNews(TicketInfo message);
    }
}
