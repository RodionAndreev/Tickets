using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Search.Logic.Common.Models;

namespace Search.Logic.Common.Fakers
{
    public class TicketInfoFaker
    {
        private static Faker<TicketInfo> _faker;

        static TicketInfoFaker()
        {
            _faker = new Faker<TicketInfo>();
            _faker.RuleFor(t => t.Date, f => f.Date.Between(DateTime.Today, DateTime.Today.AddDays(7)));
            _faker.RuleFor(t => t.Id, f => f.Lorem.Word());
            _faker.RuleFor(t => t.Category, f => f.Lorem.Word());
        }

        public static IEnumerable<TicketInfo> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}
