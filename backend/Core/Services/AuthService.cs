using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Core.Interface.Infrastructure.Cloudinary;
using Core.Interface.Infrastructure.Database;
using Core.Interface.Services;
using Core.Utility;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPhotoRespository<UserPhoto> _photoRepo;
        private readonly IAuthRespository _authRespository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IPhotoCloudinary _photoCloudinary;

        public AuthService(IAuthRespository authRespository, IMapper mapper, JwtSettings jwtSetting, IPhotoRespository<UserPhoto> photoRepo, IPhotoCloudinary photoCloudinary)
        {
            _photoCloudinary = photoCloudinary;
            _photoRepo = photoRepo;
            _authRespository = authRespository;
            _mapper = mapper;
            _jwtSettings = jwtSetting;
        }

        public Task<IEnumerable<CommunityTrip>> GetCommunityTripAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginServiceOutput> Login(LoginServiceInput input)
        {
            ApplicationUser user = await _authRespository.FindByEmail(input.Email);

            if (user == null)
            {
                throw new ArgumentException("Email or Password is not correct");
            }

            IdentityResult result = await _authRespository.CheckPassword(user, input.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException("Email or Password is not correct");
            }

            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.SecertKey);

            var role = await _authRespository.GetRolesUser(user);

            if (string.IsNullOrEmpty(role.FirstOrDefault()))
            {
                throw new ArgumentException($"User {user.UserName} has no role");
            }

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, user.UserName),
                    new (ClaimTypes.NameIdentifier, user.Id),
                    new (ClaimTypes.Email, user.Email),
                    new (ClaimTypes.Role, role.FirstOrDefault())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)// we have to pass key and algo to create token,
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            LoginServiceOutput output = new()
            {
                Email = user.Email,
                Token = tokenHandler.WriteToken(token),
                DisplayName = user.DisplayName,
                Image = user.Image != null ? user.Image.Url : null
            };

            if (string.IsNullOrEmpty(output.Email) || string.IsNullOrEmpty(output.Token))
            {
                throw new ArgumentException("Login failed");
            }
            return output;
        }

        public async Task<RegisterServiceOutput> Register(RegisterServiceInput input)
        {
            IdentityResult existedUserName = await _authRespository.ExistedEmail(input.Email);
            if (existedUserName.Errors.Any())
            {
                throw new Exception(existedUserName.Errors.First().Description);
            }

            List<Contact> contacts = input.Contacts.Select(c => new Contact
            {
                Id = Guid.NewGuid(),
                Channel = c.Channel,
                Name = c.Name
            }).ToList();

            ApplicationUser newUser = new()
            {
                UserName = input.DisplayName.ToLower(),
                Email = input.Email.ToLower(),
                DisplayName = input.DisplayName,
                Bio = input.Bio,
                Image = null,
                NormalizedUserName = input.DisplayName.ToUpper(),
                Contacts = contacts
            };

            IdentityResult result = await _authRespository.CreateAsync(newUser, input.Password);

            if (result.Succeeded)
            {
                //throw new Exception($"{JsonConvert.SerializeObject(newUser)}");

                IdentityResult res = await _authRespository.AddRoleToUser(newUser, SD.GeneralUser);

                if (!res.Succeeded || _authRespository.AddUserContact(newUser, contacts).Result < 1)
                {
                    throw new Exception("User role or contact is not added");
                }

                return new RegisterServiceOutput
                {
                    data = input,
                    Message = "success"
                };
            }

            // _authRespository.Delete(newUser);
            throw new Exception(result.Errors.First().Description);
        }

    }
}