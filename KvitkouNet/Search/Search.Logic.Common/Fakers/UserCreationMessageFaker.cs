using System;
using System.Collections.Generic;
using Bogus;
using KvitkouNet.Messages.TicketManagement;
using KvitkouNet.Messages.UserManagement;

namespace Search.Logic.Common.Fakers
{
   public class UserCreationMessageFaker
    {
        private static Faker<UserCreationMessage> _faker;

        static UserCreationMessageFaker()
        {
            _faker = new Faker<UserCreationMessage>();
            _faker.RuleFor(t => t.UserId, f => Guid.NewGuid().ToString());
            _faker.RuleFor(t => t.FirstName, f => f.Person.FirstName);
            _faker.RuleFor(t => t.LastName, f => f.Person.LastName);
            _faker.RuleFor(t => t.Email, f => f.Internet.Email());
        }

        public static IEnumerable<UserCreationMessage> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }

    }
}
