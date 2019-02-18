using Bogus;
using Notification.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Notification.Data.Fakers
{
    class UserSubscriptionFaker
    {
        private static List<Subscription> m_subscriptions = new List<Subscription>();
        private static Faker<Subscription> m_subscriptionFaker = new Faker<Subscription>();

        static UserSubscriptionFaker()
        {
        }

        public static IEnumerable<UserSubscription> Generate(int subscriptionCount = 4)
        {
            m_subscriptionFaker.RuleFor(x => x.Id, x => x.IndexFaker.ToString());
            m_subscriptionFaker.RuleFor(x => x.Theme, x => x.Random.String2(5));
            m_subscriptionFaker.RuleFor(x => x.Creator, x => x.Random.String2(5));
            m_subscriptionFaker.RuleFor(x => x.UserSubscriptions, x => new Collection<UserSubscription>());

            m_subscriptions.AddRange(m_subscriptionFaker.Generate(subscriptionCount));
                       
            Collection<UserSubscription> userSubscriptions = new Collection<UserSubscription>();

            for (int i = 0; i < m_subscriptions.Count; i++)
            {
                UserSubscription userSubscription = new UserSubscription
                {
                    Subscription = m_subscriptions[i],
                    IsSubscribed = true,
                    ClientNotificationNeeded = true,
                    EmailNotificationNeeded = false
                };
                userSubscriptions.Add(userSubscription);
            }

            return userSubscriptions;
        }
    }
}
