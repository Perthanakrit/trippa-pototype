using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
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
            ApplicationUser user = await _db.ApplicationUsers
                                            .Where(u => u.UserName.ToLower() == userName.ToLower())
                                            .Include(u => u.Contacts)
                                            .Include(u => u.Image)
                                            .FirstOrDefaultAsync();
            return user;
        }

        public async Task<ApplicationUser> FindByEmail(string email)
        {
            ApplicationUser user = await _db.ApplicationUsers
                                            .AsNoTracking()
                                            .Where(u => u.Email == email.ToLower())
                                            .Include(u => u.Image)
                                            .FirstOrDefaultAsync();

            return user;
        }
        /*
            .Include(u => u.Contacts)
                                            .Include(u => u.Image)
        */
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
            //await _db.Contacts.AddRangeAsync(entity.Contacts);
            //await _db.SaveChangesAsync();
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

        public async Task<ApplicationUser> ExistedUserId(string id)
        {
            ApplicationUser user = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<string> GetGeneralUserRole()
        {
            var res = await _db.Roles.FirstOrDefaultAsync(u => u.Name == "GeneralUser");
            return res.Name;
        }

        public async Task<IList<string>> GetRolesUser(ApplicationUser user) => await _userManager.GetRolesAsync(user);

        public async Task<IdentityResult> AddRoleToUser(ApplicationUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<int> AddUserContact(ApplicationUser user, List<Contact> contacts)
        {
            user.Contacts = contacts;
            _db.ApplicationUsers.Update(user);
            return await _db.SaveChangesAsync();
        }

        public Task<bool> IsHostInTrip(string userId, Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsHostInCommuTrip(string userId, Guid tripId)
        {
            return await _db.CommunityTripAttendees.AnyAsync(u => u.ApplicationUserId == userId && u.CommunityTripId == tripId);
        }
    }
}