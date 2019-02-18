using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Fakers
{
    public class AccountFaker
    {
        private static Faker<AccountDB> _faker;

        static AccountFaker()
        {
            _faker = new Faker<AccountDB>();
            _faker.RuleFor(x => x.Login, f => f.Lorem.Word());
            _faker.RuleFor(x => x.Password, f => f.Lorem.Word());
            _faker.RuleFor(x => x.Email, f => f.Lorem.Word());
        }

        public static IEnumerable<AccountDB> Generate(int count = 10)
        {
            return _faker.Generate(count);
        }
    }
}
