using Microsoft.EntityFrameworkCore;
using UserSettings.Data.DbModels;

namespace UserSettings.Data.Context
{
	public class SettingsContext: DbContext
	{
		public SettingsContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<SettingsDb> Settings { get; set; }

		public DbSet<NotificationDb> Notifications { get; set; }

		public DbSet<VisibleInfoDb> VisibleInformations { get; set; }
	}
}
