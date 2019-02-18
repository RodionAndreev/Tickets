using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Notification.Data.Models;

namespace Notification.Data.Context
{
	public class NotificationContext : DbContext
	{
		public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
		{	}

		public DbSet<User> Users { get; set; }

		public DbSet<Models.Notification> Notifications { get; set; }

		public DbSet<TemporaryUser> TemporaryUsers { get; set; }

		public DbSet<Subscription> Subscriptions { get; set; }

		public DbSet<UserSubscription> UserSubscriptions { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			EntityTypeBuilder<User> user = modelBuilder.Entity<User>();
			user.Property(p => p.Name).HasMaxLength(50).IsRequired();

			EntityTypeBuilder<Models.Notification> notification = modelBuilder.Entity<Models.Notification>();
			notification.Property(p => p.Text).HasMaxLength(2000).IsRequired();
			notification.Property(p => p.Title).HasMaxLength(200).IsRequired();
			notification.Property(p => p.Date).IsRequired();
			notification.Property(p => p.Severity).IsRequired();
			notification.Property(p => p.Type).IsRequired();
			notification.Property(p => p.UserId).IsRequired();

			EntityTypeBuilder<UserSubscription> userSubscription = modelBuilder.Entity<UserSubscription>();
			userSubscription.HasKey(x => new { x.UserId, x.SubscriptionId });
			userSubscription.HasOne(p => p.User)
				.WithMany(p => p.UserSubscriptions)
				.HasForeignKey(p => p.UserId);
			userSubscription.HasOne(p => p.Subscription)
				.WithMany(p => p.UserSubscriptions)
				.HasForeignKey(p => p.SubscriptionId);
		}
	}
}
