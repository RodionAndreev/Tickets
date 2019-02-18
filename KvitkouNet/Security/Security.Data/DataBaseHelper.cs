using System.Collections.Generic;
using System.Linq;
using Security.Data.ConfigModels;
using Security.Data.Models;

namespace Security.Data
{
    internal static class DataBaseHelper
    {
        public static async void CreateDefault(DefaultRulesAll rules, ISecurityData securityData)
        {
            if (rules == null)
            {
                return;
            }

            var rights = rules.Roles.Where(l=>l.AccessRights != null).SelectMany(l => l.AccessRights)
                .Union(rules.Features.Where(l => l.AvailableAccessRights != null).SelectMany(l => l.AvailableAccessRights))
                .Union(rules.AccessRights??new List<AccessRightDb>())
                .Select(l => l.Name)
                .Distinct()
                .ToArray();

            var rightsResult = (await securityData.AddRights(rights)).ToDictionary(l => l.Name, k => k.Id);

            var featureResult = new Dictionary<string, int>();
            foreach (var feature in rules.Features)
            {
                featureResult.Add(feature.Name, await securityData.AddFeature(feature.Name));
                if(feature.AvailableAccessRights != null)
                {
                    await securityData.EditFeatureRights(featureResult[feature.Name],
                    feature.AvailableAccessRights.Select(l => rightsResult[l.Name]).ToArray());
                }
            }

            var functionResult = new Dictionary<string, int>();
            foreach (var function in rules.Functions)
            {
                functionResult.Add(function.Name, await securityData.AddFunction(function.Name, featureResult[function.FeatureName]));
                if(function.AccessRights!=null)
                {
                    await securityData.EditFunctionRights(functionResult[function.Name],
                    function.AccessRights.Select(l => rightsResult[l.Name]).ToArray());
                }
            }

            foreach (var function in rules.Roles.Where(l => l.AccessFunctions != null)
                .SelectMany(l => l.AccessFunctions).Where(l => !functionResult.ContainsKey(l.Name)))
            {
                functionResult.Add(function.Name,
                    await securityData.AddFunction(function.Name, featureResult[function.FeatureName]));
                if (function.AccessRights != null)
                {
                    await securityData.EditFunctionRights(functionResult[function.Name],
                        function.AccessRights.Select(l => rightsResult[l.Name]).ToArray());
                }
            }

            var roleResult = new Dictionary<string, int>();
            foreach (var role in rules.Roles)
            {
                roleResult.Add(role.Name, await securityData.AddRole(role.Name));
                if (role.AccessFunctions != null)
                {
                    await securityData.EditRoleFunctions(roleResult[role.Name],
                        role.AccessFunctions.Select(l => functionResult[l.Name]).ToArray());
                }

                if (role.AccessRights != null || role.DeniedRights != null)
                {
                    await securityData.EditRoleRights(roleResult[role.Name],
                        role.AccessRights?.Select(l => rightsResult[l.Name]).ToArray(),
                        role.DeniedRights?.Select(l => rightsResult[l.Name]).ToArray());
                }
            }
        }
    }
}