using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Security.Logic.Models;
using Security.Logic.Models.Requests;
using Security.Logic.Models.Responses;

namespace Security.Logic.Services
{
    /// <summary>
    /// Сервис для работы с сущностями Security
    /// </summary>
    public interface IUserRightsService : IDisposable
    {
        /// <summary>
        /// Получение списка пользователей
        /// </summary>
        /// <param name="itemsPerPage">Элементов на странице</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="mask">Маска поиска</param>
        /// <returns></returns>
        Task<UserInfoResponse> GetUsersInfo(int itemsPerPage, int pageNumber, string mask = null);

        /// <summary>
        /// Получение списка прав доступа пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<UserRightsResponse> GetUserRights(string userId);
        
        /// <summary>
        /// Изменение прав доступа пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="roleIds">Набор ролей</param>
        /// <param name="functionIds">Набор функций</param>
        /// <param name="accessedRightsIds">Набор доступных прав</param>
        /// <param name="deniedRightsIds">Набор запрещённых прав</param>
        /// <returns></returns>
        Task<ActionResponse> EditUserRights(string userId, int[] roleIds, int[] functionIds, int[] accessedRightsIds, int[] deniedRightsIds);

        /// <summary>
        /// Изменение прав доступа пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="roles">Набор ролей</param>
        /// <param name="functions">Набор функций</param>
        /// <param name="accessedRights">Набор доступных прав</param>
        /// <param name="deniedRights">Набор запрещённых прав</param>
        /// <returns></returns>
        Task<ActionResponse> EditUserRightsByNames(string userId, string[] roles, string[] functions, string[] accessedRights, string[] deniedRights);

        /// <summary>
        /// Удаление пользователя из системы
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<ActionResponse> DeleteUserRights(string userId);

        /// <summary>
        /// Проверка прав доступа пользователя
        /// </summary>
        /// <param name="accessRequest">Запрос наличия доступа</param>
        /// <returns></returns>
        Task<AccessResponse> CheckAccess(CheckAccessRequest accessRequest);

        #region EventHandlers

        /// <summary>
        /// Предоставление прав доступа новому пользователю
        /// </summary>
        /// <returns></returns>
        Task<ActionResponse> SetDefaultRoleToNewUser(UserInfo userInfo, UserRights rights);

        /// <summary>
        /// Предоставление прав доступа новому пользователю
        /// </summary>
        /// <param name="userInfo">Информация пользователя</param>
        /// <returns></returns>
        Task<ActionResponse> UpdateUserInfo(UserInfo userInfo);
       
        #endregion
    }
}
