using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using IdentityServer.SecurityClient.Model;
using IdentityServer.UserManagmentClient.Model;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer
{
    public class UserManagerHelper
    {
        public static IList<Claim> GetClaims(UserRightsResponse userRightsResponse, CancellationToken cancellationToken)
        {
            var userRights = userRightsResponse.UserRights;
            if (userRights == null)
            {
                throw new InvalidOperationException(userRightsResponse.Message);
            }

            var roles = userRights.Roles.Select(l => new Claim( "role", l.Name));
            var functions = userRights.Roles.SelectMany(l => l.AccessFunctions)
                .Union(userRights.AccessFunctions).Select(l => l.FeatureName)
                .Distinct()
                .Select(l=> new Claim("function", l));

            var rights = userRights.Roles.SelectMany(l => l.AccessRights)
                .Union(userRights.AccessFunctions.SelectMany(l => l.AccessRights))
                .Union(userRights.AccessRights)
                .Select(l => l.Name)
                .Distinct()
                .Except(userRights.DeniedRights.Select(l => l.Name))
                .Select(l => new Claim("right", l));

            var result = new List<Claim>();

            result.AddRange(roles);
            result.AddRange(functions);
            result.AddRange(rights);
            return result;
        }

        internal static string GetEmail(ForViewModel userGet, CancellationToken cancellationToken)
        {
            if (userGet == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return userGet.Email;
        }

        public static bool GetEmailConfirmed(ForViewModel userGet, CancellationToken cancellationToken)
        {
            if (userGet == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return userGet.EmailConfirmed == true;
        }

        public static string GetPhoneNumber(ForViewModel userGet, CancellationToken cancellationToken)
        {
            if (userGet == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return userGet.PhoneNumber;
        }

        public static bool GetPhoneNumberConfirmed(ForViewModel userGet, CancellationToken cancellationToken)
        {
            if (userGet == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return true;
        }

        public static IdentityUser FindByName(ModelWithHashPassw userGet, CancellationToken cancellationToken)
        {
            return new IdentityUser(userGet.Login)
            {
                Email = userGet.Email,
                Id = userGet.Id,
                UserName = userGet.Login,
                PhoneNumber = userGet.PhoneNumber,
                EmailConfirmed = userGet.EmailConfirmed == true,
                PasswordHash = userGet.HashPassword
            };
        }
    }
}
