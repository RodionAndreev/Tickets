using System.Collections.Generic;
using Bogus;
using Dashboard.Data.DbModels;

namespace Dashboard.Data.Fakers
{
    public static class NewsFaker
    {
        private static readonly Faker<NewsDb> _fakerNews;

        static NewsFaker()
        {
            _fakerNews = new Faker<NewsDb>();
            _fakerNews.RuleFor(db => db.NewsId, faker => faker.Random.String(2));
            _fakerNews.RuleFor(db => db.CreatedDate, faker => faker.Date.Soon());

            _fakerNews.RuleFor(db => db.Ticket, f =>
            {
                var fakerTicketInfo = new Faker<TicketInfoDb>();
                fakerTicketInfo.RuleFor(db => db.TicketId, faker => faker.Random.String2(7));
                fakerTicketInfo.RuleFor(db => db.Name, faker => faker.Lorem.Word());
                fakerTicketInfo.RuleFor(db => db.Date, faker => faker.Date.Soon());
                fakerTicketInfo.RuleFor(db => db.City, faker => faker.Address.City());
                fakerTicketInfo.RuleFor(db => db.Category, faker => faker.Lorem.Word());
                fakerTicketInfo.RuleFor(db => db.Price, faker => faker.Finance.Amount(1, 1000, 2));
                return fakerTicketInfo.Generate();
            });
        }

        public static IEnumerable<NewsDb> Generate(int count =10)
        {
            return _fakerNews.Generate(count);
        }
    }
}