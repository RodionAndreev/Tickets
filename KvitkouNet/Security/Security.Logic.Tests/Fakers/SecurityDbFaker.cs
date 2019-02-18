using System.Collections.Generic;
using System.Linq;
using Security.Data.Models;

namespace Security.Logic.Tests.Fakers
{
    class SecurityDbFaker
    {
        public SecurityDbFaker()
        {
            Features = FeatureDbFaker.Generate(50)
                .GroupBy(l => l.Name).Select(l => l.First()).ToList();
            Functions = new List<AccessFunctionDb>();
            foreach (var feature in Features)
            {
                var featureFunctions = AccessFunctionDbFaker.Generate(10)
                    .GroupBy(l => l.Name).Select(l => l.First()).ToList();
                foreach (var function in featureFunctions)
                {
                    function.AccessRights = AccessRightDbFaker
                        .Generate(feature.Name, function.Name, 5)
                        .GroupBy(l => l.Name).Select(l => l.First()).ToList();
                }
                Functions.AddRange(featureFunctions);

                feature.AvailableAccessRights = featureFunctions.SelectMany(l => l.AccessRights).ToList();
            }

            AccessRights = Features.SelectMany(l => l.AvailableAccessRights).ToList();

            Roles = RoleDbFaker.Generate(Functions, 15);
            foreach (var role in Roles)
            {
                var rights = AccessRightDbFaker
                    .Generate(count: 5).GroupBy(l => l.Name).Select(l => l.First()).ToList();
                role.AccessRights = rights;
                var rightsDen = AccessRightDbFaker
                    .Generate(count: 5).GroupBy(l => l.Name).Select(l => l.First()).ToList();
                role.DeniedRights = rightsDen;
                AccessRights.AddRange(rights);
                AccessRights.AddRange(rightsDen);
            }

            UserRights = UserRightsDbFaker.Generate(Roles, Functions, 10);
            foreach (var userRight in UserRights)
            {
                var rights = AccessRightDbFaker
                    .Generate(count: 5).GroupBy(l => l.Name).Select(l => l.First()).ToList();
                userRight.AccessRights = rights;
                var rightsDen = AccessRightDbFaker
                    .Generate(count: 5).GroupBy(l => l.Name).Select(l => l.First()).ToList();
                userRight.DeniedRights = rightsDen;
                AccessRights.AddRange(rights);
                AccessRights.AddRange(rightsDen);
            }
        }

        public IEnumerable<UserRightsDb> UserRights { get; set; }

        public IEnumerable<RoleDb> Roles { get; set; }

        public List<AccessRightDb> AccessRights { get; set; }

        public List<AccessFunctionDb> Functions { get; set; }

        public List<FeatureDb> Features { get; set; }
    }
}
