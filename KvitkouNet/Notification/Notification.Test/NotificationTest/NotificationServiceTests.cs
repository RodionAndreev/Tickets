using Microsoft.Extensions.Options;
using Moq;
using Notification.Data.Models;
using Notification.Data.Models.Enums;
using Notification.Logic.Configs;
using Notification.Logic.Models;
using Notification.Logic.Models.Requests;
using Notification.Logic.Services;
using Notification.Logic.Services.EmailNotificationService;
using Notification.Logic.Services.NotificationService;
using Notification.Test.Fakers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Test.NotificationTest
{
    class NotificationServiceTests : BaseTest
    {
        private INotificationService m_notificationService;
        private IEmailNotificationService m_emailNotificationService;

        private const string m_eamil = "email";
        private const string m_name = "name";
        private const string m_password = "pass";

        [SetUp]
        public void Setup()
        {
            Context.Notifications.AddRange(NotificationFaker.Generate());
            Context.SaveChanges();
            m_notificationService = new NotificationService(Context, Mapper);

            

            Mock<IOptionsMonitor<SenderConfig>> mockSenderConfig = new Mock<IOptionsMonitor<SenderConfig>>();
            mockSenderConfig.Setup(x => x.CurrentValue).Returns(new SenderConfig { Email = m_eamil, Name = m_name, Password = m_password });

            Mock<IEmailSenderService> mockSenderService = new Mock<IEmailSenderService>();
            mockSenderService.Setup(x => x.SendEmailAsync(It.IsAny<SendEmailRequest>(), It.Is<SenderConfig>((config) => config.Email == m_eamil 
                                                                                                    && config.Name == m_name 
                                                                                                    && config.Password == m_password))).Returns(Task.FromResult(false));

            m_emailNotificationService = new EmailNotificationService(Context, mockSenderService.Object, Mapper, mockSenderConfig.Object);
        }

        [Test]
        public void AddUserNotificationPositive()
        {
            const string title = "Hello";
            const string text = "My message";
            const string creator = "Notification";
            User user = Context.Users.First();

            UserNotificationBulkRequest request = new UserNotificationBulkRequest
            {
                UserIds = new List<string> { user.Id },
                Message = new Logic.Models.NotificationMessage
                {
                    Title = title,
                    Text = text,
                    Creator = creator,
                    Severity = Logic.Models.Enums.Severity.Informational
                }
            };

            m_notificationService.AddUserNotifications(request);

            Data.Models.Notification notification = Context.Notifications.SingleOrDefault(x => x.Title == title);

            Assert.IsNotNull(notification);
            Assert.AreEqual(user.Id, notification.UserId);
            Assert.AreEqual(text, notification.Text);
            Assert.AreEqual(creator, notification.Creator);
            Assert.AreEqual(NotificationType.Notification, notification.Type);
            Assert.AreEqual(false, notification.IsClosed);
        }

        [Test]
        public void SetStatusClosedPositive()
        {
            User user = Context.Users.First();

            Data.Models.Notification notification = Context.Notifications.First(x => x.IsClosed == false);

            m_notificationService.SetStatusClosed(notification.Id);

            Data.Models.Notification closedNotification = Context.Notifications.SingleOrDefault(x => x.Id == notification.Id);

            Assert.IsNotNull(notification);
            Assert.AreEqual(true, closedNotification.IsClosed);
        }

        [Test]
        public void GetUserNotificationsPositive()
        {
            User user = Context.Users.First();

            List<UserNotification> userNotifications = m_notificationService.GetUserNotifications(user.Id, true).Result.ToList();

            Assert.IsNotNull(userNotifications);
            Assert.IsTrue(userNotifications.Count > 0);
        }

        
        [Test]
        public void SendEmailNotificationsPositive()
        {
            const string title = "Email Hello";
            const string text = "Send my message";
            const string creator = "EmailNotification";
            User user = Context.Users.First();

            UserNotificationBulkRequest request = new UserNotificationBulkRequest
            {
                UserIds = new List<string> { user.Id },
                Message = new Logic.Models.NotificationMessage
                {
                    Title = title,
                    Text = text,
                    Creator = creator,
                    Severity = Logic.Models.Enums.Severity.Informational
                }
            };

            m_emailNotificationService.SendEmailNotifications(request).GetAwaiter().GetResult();

            Data.Models.Notification notification = Context.Notifications.SingleOrDefault(x => x.Title == title);

            Assert.IsNotNull(notification);
            Assert.AreEqual(user.Id, notification.UserId);
            Assert.AreEqual(text, notification.Text);
            Assert.AreEqual(creator, notification.Creator);
            Assert.AreEqual(user.Email, notification.Email);
            Assert.AreEqual(NotificationType.EmailNotification, notification.Type);
            Assert.AreEqual(true, notification.IsClosed);
        }
    }
}
