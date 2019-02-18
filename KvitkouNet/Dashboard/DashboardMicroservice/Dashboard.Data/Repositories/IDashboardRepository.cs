using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Data.DbModels;

namespace Dashboard.Data.Repositories
{
    public interface IDashboardRepository
    {
        /// <summary>
        ///     Добавляет новость в БД
        /// </summary>
        /// <param name="news">Модель новости</param>
        /// <returns>Код ответа Create и добавленную модель</returns>
        Task<string> Add(NewsDb news);

        /// <summary>
        ///     Удаление всех новостей в БД
        /// </summary>
        /// <returns></returns>
        Task DeleteAll();

        /// <summary>
        ///     Удаление новости с определенным Id в БД
        /// </summary>
        /// <param name="newsId"></param>
        /// <returns></returns>
        Task Delete(string newsId);

        /// <summary>
        ///     Получение всех новостей имеющихся в системе в БД
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<NewsDb>> GetAll();

        /// <summary>
        ///     Получение новости по Id в БД
        /// </summary>
        /// <param name="newsId">Id новости</param>
        /// <returns></returns>
        Task<NewsDb> Get(string newsId);

        /// <summary>
        ///     Автоматическое добовление новости
        /// </summary>
        /// <returns></returns>
        Task AddAutoNews(NewsDb message);

    }
}
