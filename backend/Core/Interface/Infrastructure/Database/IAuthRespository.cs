using Core.Interface.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interface.Infrastructure.Database
{
    public interface IAuthRespository
    {
        Task<IdentityResult> CreateAsync(ApplicationUser entity, string password);
        Task<IdentityResult> ExistedUserName(string userName);
        Task<IdentityResult> ExistedEmail(string email);
        Task<ApplicationUser> FindByUsername(string userName);
        Task<ApplicationUser> FindByEmail(string email);
        Task<IdentityResult> CheckPassword(ApplicationUser entity, string password);
        Task<bool> ExistedUserId(string id);
        Task<string> GetGeneralUserRole();
        Task<IList<string>> GetRolesUser(ApplicationUser user);
        Task<IdentityResult> AddRoleToUser(ApplicationUser user, string role);
        Task<int> AddUserContact(ApplicationUser user, List<Contact> contacts);
    }
}