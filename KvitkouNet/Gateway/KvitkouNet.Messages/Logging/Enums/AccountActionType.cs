using System;

namespace KvitkouNet.Messages.Logging.Enums
{
	/// <summary>
	/// Перечисление, описывающее действие с пользовательским аккаунтом
	/// </summary>
	[Flags]
	public enum AccountActionType
	{
		/// <summary>
		/// Неизвестный тип
		/// </summary>
		Unknown = 0,

		/// <summary>
		/// Вход в аккаунт
		/// </summary>
		SignIn = 1 << 0,

		/// <summary>
		/// Неудачная попытка входа
		/// </summary>
		FailedLogin = 1 << 1,

		/// <summary>
		/// Выход из аккаунта
		/// </summary>
		LogOut = 1 << 2,

		/// <summary>
		/// Регистрация аккаунта
		/// </summary>
		Registration = 1 << 3,

		/// <summary>
		/// Сокрытие аккаунта
		/// </summary>
		Trash = 1 << 4,

		/// <summary>
		/// Бан аккаунта
		/// </summary>
		Ban = 1 << 5,

		/// <summary>
		/// Восстановление аккаунта
		/// </summary>
		Recovery = 1 << 6,

		/// <summary>
		/// Восстановления пароля от аккаунта
		/// </summary>
		PasswordRecovery = 1 << 7,

		/// <summary>
		/// Удаление аккаунта
		/// </summary>
		Delete = 1 << 8
	}
}