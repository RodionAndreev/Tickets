using System;
using System.Threading.Tasks;
using Security.Logic.Models.Responses;

namespace Security.Logic.Services
{
    /// <summary>
    /// Сервис для работы с сущностями Security
    /// </summary>
    public interface IFunctionService : IDisposable
    {
        /// <summary>
        /// Получение списка функций
        /// </summary>
        /// <param name="itemsPerPage">Элементов на странице</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="mask">Маска поиска</param>
        /// <returns></returns>
        Task<AccessFunctionResponse> GetFunctions(int itemsPerPage, int pageNumber, string mask = null);

        /// <summary>
        /// Добавление функции
        /// </summary>
        /// <param name="functionName">Имя функции</param>
        /// <param name="featureId">Идентификатор фичи</param>
        /// <returns></returns>
        Task<ActionResponse> AddFunction(string functionName, int featureId);

        /// <summary>
        /// Удаление функции
        /// </summary>
        /// <param name="functionId">Идентификатор функции</param>
        /// <returns></returns>
        Task<ActionResponse> DeleteFunction(int functionId);

        /// <summary>
        /// Изменение функции
        /// </summary>
        /// <param name="functionId">Идентификатор функции</param>
        /// <param name="rightsId">Набор прав</param>
        /// <returns></returns>
        Task<ActionResponse> EditFunctionRights(int functionId, int[] rightsId);
    }
}