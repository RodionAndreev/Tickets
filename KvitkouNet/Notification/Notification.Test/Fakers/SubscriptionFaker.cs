using Bogus;
using Notification.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Notification.Test.Fakers
{
    class SubscriptionFaker
    {        
        private static List<Subscription> m_subscriptions = new List<Subscription>();
        private static Faker<Subscription> m_subscriptionFaker = new Faker<Subscription>();
        private static Faker<User> m_userFaker = new Faker<User>();
        private static Faker<UserSubscription> m_userSubscriptionsFaker = new Faker<UserSubscription>();

        static SubscriptionFaker()
        {     
        }

        public static IEnumerable<Subscription> Generate(int userCount = 4, int subscriptionCount = 10)
        {
            m_userFaker.RuleFor(x => x.Id, x => x.IndexFaker.ToString());
            m_userFaker.RuleFor(x => x.Email, x => x.Internet.Email());
            m_userFaker.RuleFor(x => x.Name, x => x.Name.FirstName());

            List<User> users = m_userFaker.Generate(userCount);

            m_subscriptionFaker.RuleFor(x => x.Id, x => x.IndexFaker.ToString());
            m_subscriptionFaker.RuleFor(x => x.Theme, x => x.Random.String2(5));
            m_subscriptionFaker.RuleFor(x => x.Creator, x => x.Random.String2(5));
            m_subscriptionFaker.RuleFor(x => x.UserSubscriptions, x => new Collection<UserSubscription>());

            m_subscriptions.AddRange(m_subscriptionFaker.Generate(subscriptionCount));


            m_userSubscriptionsFaker.RuleFor(x => x.Subscription, x => m_subscriptions[x.Random.Number(m_subscriptions.Count - 1)]);
            m_userSubscriptionsFaker.RuleFor(x => x.User, x => users[x.Random.Number(users.Count - 1)]);

            Collection<UserSubscription> col = new Collection<UserSubscription>();

            for (int i = 0; i < m_subscriptions.Count; i++)
            {
                UserSubscription userSubscription;
                do
                {
                    userSubscription = m_userSubscriptionsFaker.Generate(1)[0];

                } while (col.FirstOrDefault(x => x.User.Id == userSubscription.User.Id && x.Subscription.Id == userSubscription.Subscription.Id) != null);
                col.Add(userSubscription);
                Subscription subscription = m_subscriptions.First(x => x.Id == userSubscription.Subscription.Id);
                subscription.UserSubscriptions.Add(userSubscription);
            }

            return m_subscriptions;
        }
    }
}
