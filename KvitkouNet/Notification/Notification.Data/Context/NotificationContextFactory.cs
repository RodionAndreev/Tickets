using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Notification.Data.Context
{
	/// <summary>
	/// Фабрика для создания NotificationContext
	/// </summary>
	public class NotificationContextFactory : IDesignTimeDbContextFactory<NotificationContext>
	{
		public NotificationContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<NotificationContext>();
			optionsBuilder.UseSqlite("Data Source = ./Notification.db");

			return new NotificationContext(optionsBuilder.Options);
		}
	}
}
