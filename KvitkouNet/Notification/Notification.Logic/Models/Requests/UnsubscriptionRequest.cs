using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Logic.Models.Requests
{
    /// <summary>
    /// Запрос на отписку пользователя
    /// </summary>
    public class UnsubscriptionRequest
    {
        /// <summary>
        /// Ид пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        public string Theme { get; set; }
    }
}
