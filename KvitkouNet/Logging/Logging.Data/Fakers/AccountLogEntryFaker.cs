using System.Collections.Generic;
using Bogus;
using Logging.Data.DbModels;

namespace Logging.Data.Fakers
{
    public class AccountLogEntryFaker
    {
        private static Faker<AccountLogEntryDbModel> _faker;

        static AccountLogEntryFaker()
        {
            _faker = new Faker<AccountLogEntryDbModel>();
            _faker.RuleFor(_ => _.Id, f => f.IndexFaker.ToString());
            _faker.RuleFor(_ => _.UserId, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.UserName, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.Email, f => f.Lorem.Sentence(wordCount: 5));
            _faker.RuleFor(_ => _.Type, f => f.Random.ArrayElement(new [] {8, 16 , 32}));
            _faker.RuleFor(_ => _.EventDate, f => f.Date.Recent());
            _faker.RuleFor(_ => _.Created, f => f.Date.Recent());
        }

        public static IEnumerable<AccountLogEntryDbModel> Generate(int count = 100)
            => _faker.Generate(count);
    }
}
