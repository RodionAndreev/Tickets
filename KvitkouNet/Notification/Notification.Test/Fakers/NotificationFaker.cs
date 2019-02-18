using Bogus;
using Notification.Data.Models;
using Notification.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Test.Fakers
{
    internal class NotificationFaker
    {
        private static Faker<Data.Models.Notification> m_faker;


        static NotificationFaker()
        {
            m_faker = new Faker<Data.Models.Notification>();

            Faker<User> userFaker = new Faker<User>();
            userFaker.RuleFor(x => x.Email, x => x.Internet.Email());
            userFaker.RuleFor(x => x.Name, x => x.Name.FirstName());
            User user = userFaker.Generate(1)[0];

            m_faker.RuleFor(x => x.User, x => user);
            m_faker.RuleFor(x => x.Title, x => x.Lorem.Sentence(10));
            m_faker.RuleFor(x => x.Text, x => x.Lorem.Sentence(50));
            m_faker.RuleFor(x => x.Severity, x => x.Random.Enum<Severity>());
            m_faker.RuleFor(x => x.Date, x => x.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now.AddDays(-5)));
            m_faker.RuleFor(x => x.Type, x => x.Random.Enum<NotificationType>());
            m_faker.RuleFor(x => x.IsClosed, x => x.Random.Bool());
            m_faker.RuleFor(x => x.Creator, x => x.Random.String(5));
            m_faker.RuleFor(x => x.Email, x => x.Internet.Email());
        }

        public static IEnumerable<Data.Models.Notification> Generate(int count = 10)
        {            
            return m_faker.Generate(count);
        }
    }
}
