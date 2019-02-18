using System.Collections.Generic;
using Bogus;
using Chat.Data.DbModels;

namespace Chat.Data.Fakers
{
    public static class RoomFaker
    {
        private static Faker<RoomDb> _faker;

        static RoomFaker()
        {
            _faker = new Faker<RoomDb>();
            _faker.RuleFor(x => x.Name, f => f.Lorem.Sentence(10));
            _faker.RuleFor(x => x.OwnerId, f => f.Random.Int(10, 100).ToString());
            _faker.RuleFor(x => x.IsPrivat, f => f.Random.Bool());
            _faker.RuleFor(x => x.Id, f => f.Lorem.Sentence(10));
        }

        public static IEnumerable<RoomDb> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}
