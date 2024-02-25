using Core.Interface.Infrastructure.Cloudinary;
using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoCloudinary _photoCloudinary;
        private readonly IPhotoRespository<UserPhoto> _userPhotoRepo;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhotoService(IPhotoCloudinary photoCloudinary, IPhotoRespository<UserPhoto> userPhotoRepo,
                                UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _userManager = userManager;
            _photoCloudinary = photoCloudinary;
            _userPhotoRepo = userPhotoRepo;
        }

        public Task<PhotoUploadOutput> TripAddPhoto(IFormFile file, string tripId)
        {
            throw new NotImplementedException();
        }

        public Task<string> TripDeletePhoto(string tripId, string publicId)
        {
            throw new NotImplementedException();
        }

        public async Task<PhotoUploadOutput> UserAddPhoto(IFormFile file)
        {
            string currentUserId = _userAccessor.GetUserId();
            ApplicationUser user = await _userManager.FindByIdAsync(currentUserId);
            //throw new ArgumentException($"{JsonConvert.SerializeObject(user)}");
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            UploadPhotoResult result = await _photoCloudinary.AddAsync(file);

            PhotoUploadOutput photo = new()
            {
                PublicId = result.PublicId,
                Url = result.Url
            };

            UserPhoto userPhoto = new()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                PublicId = photo.PublicId,
                Url = photo.Url,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            if (user.Image != null)
            {
                await _photoCloudinary.DeletePhotoAsync(user.UserPhotoId.ToString());
            }

            bool isInvoke = await _userPhotoRepo.AddAsync(userPhoto);
            if (!isInvoke)
            {
                throw new ArgumentException("Error when adding photo");
            }

            user.Image = userPhoto;
            user.UserPhotoId = userPhoto.Id;

            await _userManager.UpdateAsync(user);

            return photo;
        }

        public Task<string> UserDeletePhoto(string userId, string publicId)
        {
            throw new NotImplementedException();
        }
    }
}