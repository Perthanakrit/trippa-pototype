using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class AuthRespository : IAuthRespository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _db;

        public AuthRespository(UserManager<ApplicationUser> userManager, DatabaseContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<ApplicationUser> FindByUsername(string userName)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == userName);
            return user;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            return user;
        }

        public async Task<IdentityResult> CheckPassword(ApplicationUser entity, string password)
        {
            bool isPasswordValid = await _userManager.CheckPasswordAsync(entity, password);

            if (isPasswordValid)
            {
                return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError { Description = "Username or Password is not correct" });
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser entity, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(entity, password);
            await _db.Contacts.AddRangeAsync(entity.Contacts);
            return result;
        }

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

        public async Task<bool> ExistedUserId(string id)
        {
            var user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}