using Logging.Logic.Enums;
using Logging.Logic.Models.Abstraction;

namespace Logging.Logic.Models
{
	/// <summary>
	/// Запись в лог, описывающая действие с аккаунтом пользователя
	/// </summary>
	public class AccountLogEntry : BaseLogEntry
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
