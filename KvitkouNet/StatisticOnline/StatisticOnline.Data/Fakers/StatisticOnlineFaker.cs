using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using StatisticOnline.Data.Models;

namespace StatisticOnline.Data.Fakers
{
    public class StatisticOnlineFaker
    {
        private static Faker<OnlineDb> _faker;

        static StatisticOnlineFaker()
        {
            _faker = new Faker<OnlineDb>();
            _faker.RuleFor(online => online.CountAll, faker => faker.Random.Int(100, 200));
            _faker.RuleFor(online => online.CountRegistered, faker => faker.Random.Int(0, 100));
            _faker.RuleFor(online => online.CountGuest, faker => faker.Random.Int(0, 100));
            _faker.RuleFor(online => online.CreateTime, faker => faker.Date.Past(3));
        }

        public static IEnumerable<OnlineDb> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}

