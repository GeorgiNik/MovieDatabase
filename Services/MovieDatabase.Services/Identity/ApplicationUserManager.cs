﻿namespace MovieDatabase.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    using MovieDatabase.Data.Models;

    public class ApplicationUserManager<TUser> : UserManager<ApplicationUser>
        where TUser : ApplicationUser
    {
        public ApplicationUserManager(
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<IdentityResult> ActivateUserAsync(string userId)
        {
            if (userId == null)
            {
                throw new InvalidOperationException("userId");
            }

            ApplicationUser user = await this.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("user");
            }

            user.IsActive = true;

            return await this.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeactivateUserAsync(string userId)
        {
            if (userId == null)
            {
                throw new InvalidOperationException("userId");
            }

            ApplicationUser user = await this.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("user");
            }

            user.IsActive = false;

            return await this.UpdateAsync(user);
        }

        public async Task<IdentityResult> UnlockUserAsync(string userId)
        {
            if (userId == null)
            {
                throw new InvalidOperationException("userId");
            }

            ApplicationUser user = await this.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("user");
            }

            user.LockoutEnd = DateTime.UtcNow;

            return await this.UpdateAsync(user);
        }

        public async Task<IdentityResult> SetEmailConfirmationTokenResentOnAsync(string userId)
        {
            if (userId == null)
            {
                throw new InvalidOperationException("userId");
            }

            ApplicationUser user = await this.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException("user");
            }

            user.EmailConfirmationTokenResentOn = DateTime.UtcNow;

            return await this.UpdateAsync(user);
        }
    }
}
