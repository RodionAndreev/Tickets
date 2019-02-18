using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Messages.UserSettings
{
	public class EmailUpdateMessage
	{
		/// <summary>
		/// Электронный адрес пользователя
		/// </summary>
		public string Email { get; set; }

		public string UserId { get; set; }
	}
}
