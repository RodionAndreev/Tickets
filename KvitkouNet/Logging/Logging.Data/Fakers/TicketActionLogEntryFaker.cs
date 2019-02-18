using System.Collections.Generic;
using Bogus;
using Logging.Data.DbModels;

namespace Logging.Data.Fakers
{
    public class TicketActionLogEntryFaker
    {
        private static Faker<TicketActionLogEntryDbModel> _faker;

        static TicketActionLogEntryFaker()
        {
            _faker = new Faker<TicketActionLogEntryDbModel>();
            _faker.RuleFor(_ => _.Id, f => f.IndexFaker.ToString());
            _faker.RuleFor(_ => _.UserId, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.TicketId, f => f.Lorem.Word().ToString());
            _faker.RuleFor(_ => _.TicketName, f => f.Lorem.Sentence(wordCount: 5));
            _faker.RuleFor(_ => _.Type, f => f.Random.ArrayElement(new[] { 8, 16, 32 }));
            _faker.RuleFor(_ => _.Description, f => f.Lorem.Sentence(wordCount: 5));
            _faker.RuleFor(_ => _.EventDate, f => f.Date.Recent());
            _faker.RuleFor(_ => _.Created, f => f.Date.Recent());
        }

        public static IEnumerable<TicketActionLogEntryDbModel> Generate(int count = 100)
            => _faker.Generate(count);
    }
}
