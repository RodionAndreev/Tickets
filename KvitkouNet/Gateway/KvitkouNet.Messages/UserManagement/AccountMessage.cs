using System;

using KvitkouNet.Messages.Logging.Enums;

namespace KvitkouNet.Messages.UserManagement
{
	/// <summary>
	/// Модель сообщения о действии с аккаунтом пользователя
	/// </summary>
	public class AccountMessage
	{
		/// <summary>
		/// Дата события
		/// </summary>
		public DateTime Date { get; set; } = DateTime.Now;

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
