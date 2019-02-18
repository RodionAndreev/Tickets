using System.Collections.Generic;

namespace Security.Data.Models
{
    /// <summary>
    /// Роль
    /// </summary>
    public class RoleDb
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
        public List<AccessRightDb> AccessRights { get; set; }

        /// <summary>
        /// Список запрещённых прав
        /// </summary>
        public List<AccessRightDb> DeniedRights { get; set; }

        /// <summary>
        /// Список предоставляемых функций
        /// </summary>
        public List<AccessFunctionDb> AccessFunctions { get; set; }
    }
}
