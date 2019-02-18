using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Messages.UserManagement
{
	/// <summary>
	/// Сообщение для регистрации пользователя
	/// </summary>
	/// <remarks>Сообщение RegistrationMessage</remarks>
	public class RegistrationMessage
	{
		/// <summary>
		/// Имя
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Почта
		/// </summary>
		public string Email { get; set; }		
	}
}
