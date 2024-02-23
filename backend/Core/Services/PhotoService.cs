using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Cloudinary;
using Core.Interface.Infrastructure.Database;
using Core.Interface.security;
using Core.Interface.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoCloudinary _photoCloudinary;
        private readonly IPhotoRespository<Photo> _photoRespository;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public PhotoService(IPhotoCloudinary photoCloudinary, IPhotoRespository<Photo> photoRespository,
                                UserManager<ApplicationUser> userManager, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _userManager = userManager;
            _photoCloudinary = photoCloudinary;
            _photoRespository = photoRespository;
        }

        public Task<PhotoUploadOutput> TripAddPhoto(IFormFile file, string tripId)
        {
            throw new NotImplementedException();
        }

        public Task<string> TripDeletePhoto(string tripId, string publicId)
        {
            throw new NotImplementedException();
        }

        public async Task<PhotoUploadOutput> UserAddPhoto(IFormFile file, string userId)
        {
            string currentUserId = _userAccessor.GetUserId();
            ApplicationUser user = await _userManager.FindByIdAsync(currentUserId);

            UploadPhotoResult result = await _photoCloudinary.AddAsync(file);

            PhotoUploadOutput photo = new()
            {
                PublicId = result.PublicId,
                Url = result.Url
            };

            UserPhoto userPhoto = new()
            {
                Id = Guid.NewGuid(),
                PublicId = photo.PublicId,
                Url = photo.Url,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

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