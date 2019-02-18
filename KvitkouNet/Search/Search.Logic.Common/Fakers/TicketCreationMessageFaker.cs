using System;
using System.Collections.Generic;
using Bogus;
using KvitkouNet.Messages.TicketManagement;

namespace Search.Logic.Common.Fakers
{
   public class TicketCreationMessageFaker
    {
        private static Faker<TicketCreationMessage> _faker;

        static TicketCreationMessageFaker()
        {
            _faker = new Faker<TicketCreationMessage>();
            _faker.RuleFor(t => t.TicketId, f => Guid.NewGuid().ToString());
            _faker.RuleFor(t => t.City, f => f.Lorem.Word());
            _faker.RuleFor(t => t.Category, f => f.Lorem.Word());
            _faker.RuleFor(t => t.Name, f => f.Lorem.Word());
            _faker.RuleFor(t => t.Price, f => f.Random.Decimal());
            _faker.RuleFor(t => t.Date, f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(7)));
        }

        public static IEnumerable<TicketCreationMessage> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }

    }
}
