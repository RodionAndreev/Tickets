using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserSettings.Web.Models
{
	public class ProfileDto
	{
		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }
		public DateTime Birthday { get; set; }
	}
}
