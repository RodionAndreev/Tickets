using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Messages.UserManagement
{
    /// <summary>
    /// Модель сообщения о создании нового пользователя
    /// </summary>
    public class UserCreationMessage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Дата и время создания нового пользователя
        /// </summary>
        public DateTime Created { get; set; }
    }
}
