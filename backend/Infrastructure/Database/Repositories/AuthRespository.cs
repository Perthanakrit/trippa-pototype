using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Database.Repositories
{
    public class AuthRespository : IAuthRespository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRespository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> CheckPassword(ApplicationUser entity, string password)
        {
            bool isPasswordValid = await _userManager.CheckPasswordAsync(entity, password);

            if (isPasswordValid)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = "Password is not correct" });
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser entity, string password) => await _userManager.CreateAsync(entity, password);

        public async Task<IdentityResult> ExistedEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already exists" });
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> ExistedUserName(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Username already exists" });
            }
            return IdentityResult.Success;
        }
    }
}