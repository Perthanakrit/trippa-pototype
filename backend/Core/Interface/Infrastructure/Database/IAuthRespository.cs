using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interface.Infrastructure.Database
{
    public interface IAuthRespository
    {
        Task<IdentityResult> CreateAsync(ApplicationUser entity, string password);
        Task<IdentityResult> ExistedUserName(string userName);
        Task<IdentityResult> ExistedEmail(string email);
        Task<IdentityResult> CheckPassword(ApplicationUser entity, string password);
    }
}