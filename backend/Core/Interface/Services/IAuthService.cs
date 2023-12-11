using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Core.Interface.Services
{
    public interface IAuthService
    {
        Task<LoginServiceOutput> Login(LoginServiceInput input);
        Task<RegisterServiceOutput> Register(RegisterServiceInput input);
    }

    public class LoginServiceInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterServiceInput
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public ContactInput Contacts { get; set; }
    }

    public class ContactInput
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Line { get; set; }
    }

    public class RegisterServiceOutput
    {
        public RegisterServiceInput data { get; set; }
        public string Message { get; set; }
    }

    public class LoginServiceOutput
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}