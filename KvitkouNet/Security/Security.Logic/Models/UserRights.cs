using System.Collections.Generic;

namespace Security.Logic.Models
{
    /// <summary>
    /// Права пользователя
    /// </summary>
    public class UserRights
    {
        /// <summary>
        /// Идентификатор пользователя (null для незарегистрированного пользователя)
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Список предоставленных пользователю прав
        /// </summary>
        public List<AccessRight> AccessRights { get; set; }

        /// <summary>
        /// Список запрещенных пользователю прав
        /// </summary>
        public List<AccessRight> DeniedRights { get; set; }

        /// <summary>
        /// Список предоставленных пользователю функций
        /// </summary>
        public List<AccessFunction> AccessFunctions { get; set; }

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public List<Role> Roles { get; set; }
    }
}
