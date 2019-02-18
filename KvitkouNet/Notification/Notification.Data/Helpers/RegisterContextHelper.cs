using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Notification.Data.Context;
using Notification.Data.Fakers;
using Notification.Data.Models;

namespace Notification.Data.Helpers
{
	public class RegisterContextHelper
	{
		private string m_dataSource = "Data Source=./Notification.db";

		public RegisterContextHelper()
		{
			var o = new DbContextOptionsBuilder<NotificationContext>();
			o.UseSqlite(m_dataSource);

			using (var ctx = new NotificationContext(o.Options))
			{
				ctx.Database.Migrate();

				if (!ctx.Users.Any())
				{
                    //создадим тестового пользователя номер 1. Он будет владельцем комнаты номер 1 и ему пренадлежит сообщение номер 1.
                    User user = ctx.Users.Add(new Models.User()
                    {
                        Id = "5BE86359-073C-434B-AD2D-A3932222DABE",
                        Name = "Тестовый пользователь номер 1",
                        Email = "artsiom.hlotau@gmail.com"                        
                    }).Entity;

                    user.Notifications = new List<Models.Notification>();

                    List<Models.Notification> notifications = new List<Models.Notification>(NotificationFaker.Generate(25));
                    foreach(Models.Notification notification in notifications)
                    {
                        notification.UserId = user.Id;
                        user.Notifications.Add(notification);                        
                    }
                    user.UserSubscriptions = new List<UserSubscription>();

                    List<UserSubscription> userSubscriptions = new List<UserSubscription>(UserSubscriptionFaker.Generate(10));
                    foreach (UserSubscription userSubscription in userSubscriptions)
                    {
                        user.UserSubscriptions.Add(userSubscription);
                    }

                    ctx.SaveChanges();
				}
			}
		}

		public Action<DbContextOptionsBuilder> GetOptionsBuilder()
		{
			return opt => opt.UseSqlite(connectionString: m_dataSource);
		}
	}
}