using System.Collections.Generic;
using Bogus;
using Logging.Data.DbModels;

namespace Logging.Data.Fakers
{
    public class SearchQueryLogEntryFaker
    {
        private static Faker<SearchQueryLogEntryDbModel> _faker;

        static SearchQueryLogEntryFaker()
        {
            _faker = new Faker<SearchQueryLogEntryDbModel>();
            _faker.RuleFor(_ => _.Id, f => f.IndexFaker.ToString());
            _faker.RuleFor(_ => _.UserId, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.SearchCriterium, f => f.Lorem.Word().ToString());
            _faker.RuleFor(_ => _.FilterInfo, f => f.Lorem.Sentence(wordCount: 5));
            _faker.RuleFor(_ => _.EventDate, f => f.Date.Recent());
            _faker.RuleFor(_ => _.Created, f => f.Date.Recent());
        }

        public static IEnumerable<SearchQueryLogEntryDbModel> Generate(int count = 100)
            => _faker.Generate(count);
    }
}
