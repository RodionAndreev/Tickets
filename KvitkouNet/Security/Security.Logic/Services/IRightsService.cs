using System;
using System.Threading.Tasks;
using Security.Logic.Models;
using Security.Logic.Models.Responses;

namespace Security.Logic.Services
{
    /// <summary>
    /// Сервис для работы с сущностями Security
    /// </summary>
    public interface IRightsService : IDisposable
    {
        /// <summary>
        /// Получение списка прав доступа
        /// </summary>
        /// <param name="itemsPerPage">Элементов на странице</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="mask">Маска поиска</param>
        /// <returns></returns>
        Task<AccessRightResponse> GetRights(int itemsPerPage, int pageNumber, string mask = null);

        /// <summary>
        /// Добавление прав доступа
        /// </summary>
        /// <param name="rightsNames">Имена прав</param>
        /// <returns></returns>
        Task<AccessRightResponse> AddRights(string[] rightsNames);

        /// <summary>
        /// Удаление прав доступа
        /// </summary>
        /// <param name="rightId">Идентификатор права доступа</param>
        /// <returns></returns>
        Task<ActionResponse> DeleteRight(int rightId);
    }
}
