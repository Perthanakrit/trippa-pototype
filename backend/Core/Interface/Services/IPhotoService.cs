using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Services
{
    public interface IPhotoService
    {
        Task<PhotoUploadOutput> UserAddPhoto(IFormFile file);
        Task<string> UserDeletePhoto(string userId, string publicId);
        Task<PhotoUploadOutput> TripAddPhoto(IFormFile file, string tripId);
        Task<string> TripDeletePhoto(string tripId, string publicId);
    }

    public class PhotoUploadInput
    {
    }

    public class PhotoUploadOutput
    {
        public string PublicId { get; set; }
        public string Url { get; set; }
    }

    public class PhotoUserOutput
    {
        public string Url { get; set; }
    }
}