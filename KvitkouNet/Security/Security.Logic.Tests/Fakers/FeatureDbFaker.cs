using System.Collections.Generic;
using Bogus;
using Security.Data.Models;

namespace Security.Logic.Tests.Fakers
{
    class FeatureDbFaker
    {
        private static Faker<FeatureDb> _faker;

        static FeatureDbFaker()
        {
            _faker = new Faker<FeatureDb>();
            _faker.RuleFor(x => x.Id, f => f.IndexFaker);
            _faker.RuleFor(x => x.Name, f => f.Lorem.Word());
        }

        public static IEnumerable<FeatureDb> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}
