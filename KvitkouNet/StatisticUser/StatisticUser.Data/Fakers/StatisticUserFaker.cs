using System.Collections.Generic;
using Bogus;
using StatisticUser.Data.DbModels;

namespace StatisticUser.Data.Fakers
{
    public class StatisticUserFaker
    {
        private static Faker<SummaryTableDB> _faker;

        static StatisticUserFaker()
        {
            _faker = new Faker<SummaryTableDB>();
            _faker.RuleFor(user => user.UserId, faker => faker.IndexFaker);
            _faker.RuleFor(user => user.UserName, faker => faker.Person.UserName.Trim());
            _faker.RuleFor(user => user.MessagesCount, faker => faker.Random.Int(0, 300));
            _faker.RuleFor(user => user.RatingNegative, faker => faker.Random.Int(-100, 0));
            _faker.RuleFor(user => user.RatingPositive, faker => faker.Random.Int(0, 100));
            _faker.RuleFor(user => user.RegistrationDate, faker => faker.Date.Past(3));
            _faker.RuleFor(user => user.LastOnWebsite, faker => faker.Date.Past(1));

        }

        public static IEnumerable<SummaryTableDB> Generate(int count = 500)
        {
            return _faker.Generate(count);
        }
    }
}