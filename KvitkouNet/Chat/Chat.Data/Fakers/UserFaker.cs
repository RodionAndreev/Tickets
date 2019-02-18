using System;
using System.Collections.Generic;
using System.Text;
using Bogus;
using Bogus.Extensions;
using Chat.Data.DbModels;


namespace Chat.Data.Fakers
{
    public static class UserFaker
    {
        private static Faker<UserDb> _faker;

        static UserFaker()
        {
            _faker = new Faker<UserDb>();
            _faker.RuleFor(x => x.Avatar, f => f.Lorem.Sentence(10));
            _faker.RuleFor(x => x.RoomId, f => f.Random.Int(10, 100).ToString());
            _faker.RuleFor(x => x.Id, f => f.Lorem.Sentence(10));
            _faker.RuleFor(x => x.Settings, f => new SettingsDb());

        }

        public static IEnumerable<UserDb> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}
