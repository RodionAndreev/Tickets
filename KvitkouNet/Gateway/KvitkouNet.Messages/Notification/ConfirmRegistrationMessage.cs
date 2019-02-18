using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Messages.Notification
{
    /// <summary>
    /// Сообщение подтверждения регистрации пользователем
    /// </summary>
    public class ConfirmRegistrationMessage
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
    }
}
