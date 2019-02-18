using System;
using System.Threading.Tasks;
using Security.Logic.Models.Responses;

namespace Security.Logic.Services
{
    /// <summary>
    /// Сервис для работы с сущностями Security
    /// </summary>
    public interface IFeatureService : IDisposable
    {
        /// <summary>
        /// Получение списка фич
        /// </summary>
        /// <param name="itemsPerPage">Элементов на странице</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="mask">Маска поиска</param>
        /// <returns></returns>
        Task<FeatureResponse> GetFeatures(int itemsPerPage, int pageNumber, string mask = null);

        /// <summary>
        /// Добавление фичи
        /// </summary>
        /// <param name="featureName">Имя фичи</param>
        /// <returns></returns>
        Task<ActionResponse> AddFeature(string featureName);

        /// <summary>
        /// Удаление фичи
        /// </summary>
        /// <param name="featureId">Идентификатор фичи</param>
        /// <returns></returns>
        Task<ActionResponse> DeleteFeature(int featureId);

        /// <summary>
        /// Изменение фичи
        /// </summary>
        /// <param name="featureId">Идентификатор фичи</param>
        /// <param name="featureRights">Набор прав</param>
        /// <returns></returns>
        Task<ActionResponse> EditFeatureRights(int featureId, int[] featureRights);
    }
}
