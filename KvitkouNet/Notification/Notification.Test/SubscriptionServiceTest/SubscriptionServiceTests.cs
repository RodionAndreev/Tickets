using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Notification.Data.Models;
using Notification.Logic.Configs;
using Notification.Logic.Models.Requests;
using Notification.Logic.Services;
using Notification.Logic.Services.EmailNotificationService;
using Notification.Logic.Services.Interfaces;
using Notification.Logic.Services.NotificationService;
using Notification.Logic.Services.SubscriptionService;
using Notification.Test.Fakers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Test.SubscriptionServiceTest
{
    class SubscriptionServiceTests : BaseTest
    {
        private ISubscriptionService m_subscriptionService;
        private INotificationService m_notificationService;
        private IEmailNotificationService m_emailNotificationService;

        private const string m_eamil = "email";
        private const string m_name = "name";
        private const string m_password = "pass";

        [SetUp]
        public void Setup()
        {
            Context.Subscriptions.AddRange(SubscriptionFaker.Generate());

            Context.SaveChanges();

            m_notificationService = new NotificationService(Context, Mapper);

            Mock<IOptionsMonitor<SenderConfig>> mockSenderConfig = new Mock<IOptionsMonitor<SenderConfig>>();
            mockSenderConfig.Setup(x => x.CurrentValue).Returns(new SenderConfig { Email = m_eamil, Name = m_name, Password = m_password });

            Mock<IEmailSenderService> mockSenderService = new Mock<IEmailSenderService>();
            mockSenderService.Setup(x => x.SendEmailAsync(It.IsAny<SendEmailRequest>(), It.Is<SenderConfig>((config) => config.Email == m_eamil
                                                                                                    && config.Name == m_name
                                                                                                    && config.Password == m_password))).Returns(Task.FromResult(false));

            m_emailNotificationService = new EmailNotificationService(Context, mockSenderService.Object, Mapper, mockSenderConfig.Object);

            m_subscriptionService = new SubscriptionService(Context, m_notificationService, m_emailNotificationService, mockSenderConfig.Object, Mapper);
        }

        [Test]
        public void SubscribePositive()
        {
            const string theme = "subscribe";
            const string creator = "service";

            User user = Context.Users.First();

            SubscriptionRequest subscriptionRequest = new SubscriptionRequest
            {
                Theme = theme,
                UserId = user.Id,
                Creator = creator
            };

            m_subscriptionService.Subscribe(subscriptionRequest);

            Subscription subscription = Context.Subscriptions.Include(x => x.UserSubscriptions).Single(x => x.Theme == theme);

            Assert.IsNotNull(subscription);
            Assert.AreEqual(theme, subscription.Theme);
            Assert.AreEqual(creator, subscription.Creator);
            Assert.IsTrue(subscription.UserSubscriptions.Count == 1);
            Assert.AreEqual(user.Id, subscription.UserSubscriptions.First().UserId);           
        }
    }
}
