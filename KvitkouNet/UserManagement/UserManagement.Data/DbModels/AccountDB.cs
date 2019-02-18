using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Data.DbModels
{
    public class AccountDB
    {
        /// <summary>
        /// Уникальный идентификатор учетной записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Электронный адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }

        #region Связи между таблицами  
        /// <summary>
        /// Пользователь
        /// </summary>
        public string UserDBId { get; set; }
        public virtual UserDB UserDB { get; set; }
        
        #endregion
    }
}
