using System;
using System.Collections.Generic;
using System.Text;

namespace KvitkouNet.Messages.UserSettings
{
	public class PasswordUpdateMessage
	{
		/// <summary>
		/// Текущий
		/// </summary>
		public string Current { get; set; }

		/// <summary>
		/// Новый
		/// </summary>
		public string NewPassword { get; set; }

		public string UserId { get; set; }

		public PasswordUpdateMessage(string current, string newPass)
		{
			Current = current;
			NewPassword = newPass;
		}
	}
}
