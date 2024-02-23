using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Services
{
    public interface IAuthService
    {
        Task<LoginServiceOutput> Login(LoginServiceInput input);
        Task<RegisterServiceOutput> Register(RegisterServiceInput input);
    }

    public class LoginServiceInput
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterServiceInput
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public List<ContactInput> Contacts { get; set; }
    }

    public class UserPhotoInput
    {
        public string Url { get; set; }
    }
    public class ContactInput
    {
        public string Channel { get; set; }
        public string Name { get; set; }
    }

    public class RegisterServiceOutput
    {
        public RegisterServiceInput data { get; set; }
        public string Message { get; set; }
    }

    public class LoginServiceOutput
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
    }


    public class UserDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public UserPhoto Image { get; set; }
        public List<ContactInput> Contacts { get; set; }
    }
}