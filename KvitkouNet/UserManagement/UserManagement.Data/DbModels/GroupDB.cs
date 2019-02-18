using System.Collections.Generic;
using UserManagement.Data.DbModels.Security;

namespace UserManagement.Data.DbModels
{
    public class GroupDB
    {
        /// <summary>
        /// Уникальный номер группы
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание назначения группы
        /// </summary>
        public string Description { get; set; }


        #region Связи между таблицами  
        /// <summary>
        /// Пользователи группы
        /// </summary>
        public virtual ICollection<UserGroupDB> UserGroups { get; set; }

        /// <summary>
        /// Роли группы
        /// </summary>
        //public virtual ICollection<RoleDB> GroupRoles { get; set; }
        #endregion  
    }
}
