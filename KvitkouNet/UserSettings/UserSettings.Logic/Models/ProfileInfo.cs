using System;
using System.Collections.Generic;
using System.Text;

namespace UserSettings.Logic.Models
{
	public class ProfileInfo
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
		/// Gets or sets the user name (login).
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Email пользователя
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Дата и время создания нового пользователя
		/// </summary>
		public DateTime Created { get; set; } = DateTime.Now;
	}
}
