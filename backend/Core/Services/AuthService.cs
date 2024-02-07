using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Core.Utility;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRespository _authRespository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IAuthRespository authRespository, IMapper mapper, JwtSettings jwtSetting)
        {
            _authRespository = authRespository;
            _mapper = mapper;
            _jwtSettings = jwtSetting;
        }

        public async Task<LoginServiceOutput> Login(LoginServiceInput input)
        {
            ApplicationUser user = await _authRespository.FindByEmail(input.Email);
            if (user == null)
            {
                throw new ArgumentException("Username or Password is not correct");
            }
            IdentityResult result = await _authRespository.CheckPassword(user, input.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException("Username or Password is not correct");
            }

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.SecertKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new ("fullName", user.DisplayName.ToString()),
                    new ("id", user.Id.ToString()),
                    new (ClaimTypes.Email, user.UserName.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)// we have to pass key and algo to create token,
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginServiceOutput output = new()
            {
                Email = user.Email,
                Token = tokenHandler.WriteToken(token)
            };

            if (string.IsNullOrEmpty(output.Email) || string.IsNullOrEmpty(output.Token))
            {
                throw new ArgumentException("Login failed");
            }
            return output;
        }

        public async Task<RegisterServiceOutput> Register(RegisterServiceInput input)
        {
            IdentityResult existedUserName = await _authRespository.ExistedUserName(input.UserName);
            if (existedUserName.Errors.Any())
            {
                throw new Exception(existedUserName.Errors.First().Description);
            }

            ApplicationUser newUser = new()
            {
                UserName = input.UserName,
                Email = input.UserName,
                DisplayName = input.DisplayName,
                Bio = input.Bio,
                Image = input.Image,
                NormalizedUserName = input.UserName.ToUpper(),

            };
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