using System.Collections.Generic;

namespace Security.Data.ContextModels
{
    /// <summary>
    /// Роль
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список прав
        /// </summary>
        public List<RoleAccessRight> RoleAccessRight { get; set; }

        /// <summary>
        /// Список предоставляемых функций
        /// </summary>
        public List<RoleAccessFunction> RoleAccessFunction { get; set; }

        /// <summary>
        /// Список ролей пользователя
        /// </summary>
        public List<UserRightsRole> UserRightsRole { get; set; }
    }
}
