using System;
using System.Threading.Tasks;
using Security.Logic.Models;
using Security.Logic.Models.Responses;

namespace Security.Logic.Services
{
    /// <summary>
    /// Сервис для работы с сущностями Security
    /// </summary>
    public interface IRoleService : IDisposable
    {
        /// <summary>
        /// Получение списка ролей
        /// </summary>
        /// <param name="itemsPerPage">Элементов на странице</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="mask">Маска поиска</param>
        /// <returns></returns>
        Task<RoleResponse> GetRoles(int itemsPerPage, int pageNumber, string mask = null);

        /// <summary>
        /// Добавление роли
        /// </summary>
        /// <param name="roleName">Имя роли</param>
        /// <returns></returns>
        Task<ActionResponse> AddRole(string roleName);

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <returns></returns>
        Task<ActionResponse> DeleteRole(int roleId);

        /// <summary>
        /// Изменение роли
        /// </summary>
        /// <param name="roleId">Идентификатор роли</param>
        /// <param name="accessRightsIds">Набор доступных прав</param>
        /// <param name="deniedRightsIds">Набор запрещённых прав</param>
        /// <param name="functionIds">Набор функций</param>
        /// <returns></returns>
        Task<ActionResponse> EditRole(int roleId, int[] accessRightsIds, int[] deniedRightsIds, int[] functionIds);
    }
}
