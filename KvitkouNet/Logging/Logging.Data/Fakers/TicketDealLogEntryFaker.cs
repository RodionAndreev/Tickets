using System.Collections.Generic;
using Bogus;
using Logging.Data.DbModels;

namespace Logging.Data.Fakers
{
    public class TicketDealLogEntryFaker
    {
        private static Faker<TicketDealLogEntryDbModel> _faker;

        static TicketDealLogEntryFaker()
        {
            _faker = new Faker<TicketDealLogEntryDbModel>();
            _faker.RuleFor(_ => _.Id, f => f.IndexFaker.ToString());
            _faker.RuleFor(_ => _.TicketId, f => f.Lorem.Word());
            _faker.RuleFor(_ => _.OwnerId, f => f.Lorem.Word());
            _faker.RuleFor(_ => _.RecieverId, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.Price, f => f.Random.Double(10, 100));
            _faker.RuleFor(_ => _.Type, f => f.Random.ArrayElement(new[] { 2, 4 }));
            _faker.RuleFor(_ => _.EventDate, f => f.Date.Recent());
            _faker.RuleFor(_ => _.Created, f => f.Date.Recent());
        }

        public static IEnumerable<TicketDealLogEntryDbModel> Generate(int count = 100)
            => _faker.Generate(count);
    }
}
