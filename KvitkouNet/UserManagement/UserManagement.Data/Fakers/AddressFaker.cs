using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Data.DbModels;

namespace UserManagement.Data.Fakers
{
    public class AddressFaker
    {
        //fakeAcc.RuleFor(x => x.Addresses, f => 
        //                        {
        //                            var fakeAddrs = new Faker<Collection<AddressDB>>();
        //var fakeAddr = new Faker<AddressDB>();
        //fakeAddr.RuleFor(x => x.City, f1 => f1.Lorem.Word());
        //                            fakeAddr.RuleFor(x => x.Country, f1 => f1.Lorem.Word());
        //                            return fakeAddrs.Generate();
        //                        });
        private static Faker<AddressDB> _faker;

        static AddressFaker()
        {
            _faker = new Faker<AddressDB>();
        }

        public static IEnumerable<AddressDB> Generate(int count = 20)
        {
            return _faker.Generate(count);
        }
    }
}
