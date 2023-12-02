using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRespository _authRespository;
        private readonly IMapper _mapper;

        public AuthService(IAuthRespository authRespository, IMapper mapper)
        {
            _authRespository = authRespository;
            _mapper = mapper;
        }

        public Task<LoginServiceOutput> Login(LoginServiceInput input)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterServiceOutput> Register(RegisterServiceInput input)
        {
            IdentityResult existedUserName = await _authRespository.ExistedUserName(input.UserName);
            if (existedUserName.Errors.Any())
            {
                throw new Exception(existedUserName.Errors.First().Description);
            }

            ApplicationUser newUser = _mapper.Map<RegisterServiceInput, ApplicationUser>(input);
            IdentityResult result = await _authRespository.CreateAsync(newUser, input.Password);
            if (result.Succeeded)
            {
                return new RegisterServiceOutput
                {
                    data = input,
                    Message = "success"
                };
            }
            throw new Exception(result.Errors.First().Description);
        }

    }

}