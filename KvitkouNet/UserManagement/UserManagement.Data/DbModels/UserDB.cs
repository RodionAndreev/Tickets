using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using UserManagement.Data.DbModels.Security;
using UserManagement.Data.DbModels.Tickets;

namespace UserManagement.Data.DbModels
{
    public class UserDB : IdentityUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        //public string Id { get; set; }

        #region Связи между таблицами  
        /// <summary>
        /// Учетная запись пользователя
        /// </summary>
        public virtual AccountDB AccountDB { get; set; }

        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public virtual ProfileDB ProfileDB { get; set; }

        /// <summary>
        /// Группы, в которых состоит пользователь
        /// </summary>
        public virtual ICollection<UserGroupDB> UserGroups { get; set; }

        /// <summary>
        /// Роли доступа пользователя
        /// </summary>
        public virtual ICollection<RoleDB> UserRoles { get; set; }

        /// <summary>
        /// Список билетов принадлежащих пользователю
        /// </summary>
        //public virtual ICollection<TicketDB> Tickets { get; set; }
        #endregion 
    }
}
