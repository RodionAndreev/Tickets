using System.Collections.Generic;
using Bogus;
using Security.Data.Models;

namespace Security.Logic.Tests.Fakers
{
    class AccessFunctionDbFaker
    {
        private static Faker<AccessFunctionDb> _faker;

        static AccessFunctionDbFaker()
        {
            _faker = new Faker<AccessFunctionDb>();
            _faker.RuleFor(x => x.Id, f => f.IndexFaker);
            _faker.RuleFor(x => x.Name, f => f.Lorem.Word());
        }

        public static IEnumerable<AccessFunctionDb> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}
