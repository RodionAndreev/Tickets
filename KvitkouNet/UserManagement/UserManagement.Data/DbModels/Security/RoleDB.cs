using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Data.DbModels.Security
{
    /// <summary>
    /// Роль
    /// </summary>
    public class RoleDB
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя роли
        /// </summary>
        public string Name { get; set; }

    }
}
