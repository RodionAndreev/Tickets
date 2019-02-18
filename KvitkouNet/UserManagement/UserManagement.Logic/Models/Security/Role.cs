using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Logic.Models.Security
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

        [NotMapped]
        /// <summary>
        /// Список предоставляемых прав
        /// </summary>
        public List<AccessRight> AccessRights { get; set; }

        [NotMapped]
        /// <summary>
        /// Список запрещённых прав
        /// </summary>
        public List<AccessRight> DeniedRights { get; set; }

        [NotMapped]
        /// <summary>
        /// Список предоставляемых функций
        /// </summary>
        public List<AccessFunction> AccessFunctions { get; set; }
    }
}
