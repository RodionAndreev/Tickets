using System.Collections.Generic;
using System.Linq;
using Bogus;
using Security.Data.Models;

namespace Security.Logic.Tests.Fakers
{
    class RoleDbFaker
    {
        private static Faker<RoleDb> _faker;

        static RoleDbFaker()
        {
            _faker = new Faker<RoleDb>();
            _faker.RuleFor(x => x.Id, f => f.IndexFaker);
            _faker.RuleFor(x => x.Name, f => f.Lorem.Word());
        }

        public static IEnumerable<RoleDb> Generate(List<AccessFunctionDb> functions, int count = 10)
        {
            _faker.RuleFor(x => x.AccessFunctions, f => functions.Where((x, i) => i % f.Random.Int(2, functions.Count) == 0).ToList());
            return _faker.Generate(count);
        }
    }
}
