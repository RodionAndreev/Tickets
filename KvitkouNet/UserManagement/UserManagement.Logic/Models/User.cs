using System.Collections.Generic;
using UserManagement.Logic.Models.Security;
using UserManagement.Logic.Models.Tickets;

namespace UserManagement.Logic.Models
{
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Учетная запись пользователя
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public Profile Profile { get; set; }

        /// <summary>
        /// Группы, в которых состоит пользователь
        /// </summary>
        public virtual ICollection<Group> UserGroups { get; set; }

        /// <summary>
        /// Роли доступа пользователя
        /// </summary>

        public virtual ICollection<Role> UserRoles { get; set; }


        /// <summary>
        /// Список билетов принадлежащих пользователю
        /// </summary>
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
