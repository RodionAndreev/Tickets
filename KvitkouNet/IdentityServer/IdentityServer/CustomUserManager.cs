
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.SecurityClient.Api;
using IdentityServer.UserManagmentClient.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
#pragma warning disable 1998

namespace IdentityServer
{
    internal class CustomUserManager : UserManager<IdentityUser>
    {
        private IUserRightsApi _userRightsApi;
        private IUserApi _userApi;

        public CustomUserManager(IUserStore<IdentityUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<IdentityUser> passwordHasher, 
            IEnumerable<IUserValidator<IdentityUser>> userValidators,
            IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, 
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, 
            IServiceProvider services, 
            ILogger<UserManager<IdentityUser>> logger,
            IUserRightsApi userRightsApi,
            IUserApi userApi) : base(
            store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            logger)
        {
            _userRightsApi = userRightsApi;
            _userApi = userApi;
        }

        public override async Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            CancellationToken cancellationToken = CancellationToken;
            return UserManagerHelper.GetClaims(_userRightsApi.UserRightsGetUserRights(user.Id), cancellationToken);
        }

        public override async Task<string> GetEmailAsync(IdentityUser user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            CancellationToken cancellationToken = CancellationToken;
            return UserManagerHelper.GetEmail(_userApi.UserGet(user.Id), cancellationToken);
        }

        public override async Task<bool> IsEmailConfirmedAsync(IdentityUser user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            CancellationToken cancellationToken = CancellationToken;
            return UserManagerHelper.GetEmailConfirmed(_userApi.UserGet(user.Id), cancellationToken);
        }

        public override async Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            CancellationToken cancellationToken = CancellationToken;
            return UserManagerHelper.GetPhoneNumber(_userApi.UserGet(user.Id), cancellationToken);
        }

       public override async Task<bool> IsPhoneNumberConfirmedAsync(IdentityUser user)
        {
            ThrowIfDisposed();
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            
            CancellationToken cancellationToken = CancellationToken;
            return UserManagerHelper.GetPhoneNumberConfirmed(_userApi.UserGet(user.Id), cancellationToken);
        }

        public override async Task<IdentityUser> FindByNameAsync(string userName)
        {
            ThrowIfDisposed();
            if (userName == null)
                throw new ArgumentNullException(nameof(userName));
            userName = NormalizeKey(userName);
            CancellationToken cancellationToken = CancellationToken;
            return UserManagerHelper.FindByName(_userApi.UserGetByLogin(userName), cancellationToken);
        }


        public override Task<IdentityUser> FindByIdAsync(string userId)
        {
            ThrowIfDisposed();
            return Store.FindByIdAsync(userId, CancellationToken);
        }
    }
}