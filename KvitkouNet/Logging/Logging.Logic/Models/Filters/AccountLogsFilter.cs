using System;
using Logging.Logic.Enums;
using Logging.Logic.Models.Filters.Abstraction;

namespace Logging.Logic.Models.Filters
{
	public class AccountLogsFilter : BaseLogFilter
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Тип действия
        /// </summary>
        public AccountActionType Type { get; set; }
	}
}
