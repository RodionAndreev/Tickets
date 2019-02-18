using System.Collections.Generic;
using System.Linq;
using Bogus;
using Security.Data.Models;

namespace Security.Logic.Tests.Fakers
{
    class UserRightsDbFaker
    {
        private static Faker<UserRightsDb> _faker;

        static UserRightsDbFaker()
        {
            _faker = new Faker<UserRightsDb>();
            _faker.RuleFor(x => x.UserId, f => f.IndexFaker.ToString());
            _faker.RuleFor(x => x.UserLogin, f => f.Lorem.Word() + f.IndexFaker.ToString());
            _faker.RuleFor(x => x.FirstName, f => f.Name.FirstName());
            _faker.RuleFor(x => x.MiddleName, f => f.Name.FirstName());
            _faker.RuleFor(x => x.LastName, f => f.Name.LastName());
        }

        public static IEnumerable<UserRightsDb> Generate(IEnumerable<RoleDb> roles, IEnumerable<AccessFunctionDb> accessFunctions, int count = 10)
        {
            _faker.RuleFor(x => x.Roles, f => roles.Where((x, i) => i % f.Random.Int(2, roles.Count()) == 0).ToList());
            _faker.RuleFor(x => x.AccessFunctions, f => accessFunctions.Where((x, i) => i % f.Random.Int(2, accessFunctions.Count()) == 0).ToList());
            return _faker.Generate(count);
        }
    }
}
