using System.Collections.Generic;
using Bogus;
using Security.Data.Models;

namespace Security.Logic.Tests.Fakers
{
    static class AccessRightDbFaker
    {
        private static Faker<AccessRightDb> _faker;

        static AccessRightDbFaker()
        {
            _faker = new Faker<AccessRightDb>();
            _faker.RuleFor(x => x.Id, f => f.IndexFaker);
        }

        public static IEnumerable<AccessRightDb> Generate(string featureName = null, string functionName = null, int count = 10)
        {
            _faker.RuleFor(x => x.Name, f => $"{featureName}.{functionName??f.IndexFaker.ToString()}.{f.Lorem.Word()}");
            return _faker.Generate(count); 
        }
    }
}
