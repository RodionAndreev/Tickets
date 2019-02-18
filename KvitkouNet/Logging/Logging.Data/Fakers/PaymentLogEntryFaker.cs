using System.Collections.Generic;
using Bogus;
using Logging.Data.DbModels;

namespace Logging.Data.Fakers
{
    public class PaymentLogEntryFaker
    {
        private static Faker<PaymentLogEntryDbModel> _faker;

        static PaymentLogEntryFaker()
        {
            _faker = new Faker<PaymentLogEntryDbModel>();
            _faker.RuleFor(_ => _.Id, f => f.IndexFaker.ToString());
            _faker.RuleFor(_ => _.SenderId, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.RecieverId, f => f.Name.FirstName());
            _faker.RuleFor(_ => _.Transfer, f => f.Random.Double(10, 100));
            _faker.RuleFor(_ => _.EventDate, f => f.Date.Recent());
            _faker.RuleFor(_ => _.Created, f => f.Date.Recent());
        }

        public static IEnumerable<PaymentLogEntryDbModel> Generate(int count = 100)
            => _faker.Generate(count);
    }
}
