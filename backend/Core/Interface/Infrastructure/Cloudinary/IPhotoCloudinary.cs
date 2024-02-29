using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Infrastructure.Cloudinary
{
    public interface IPhotoCloudinary
    {
        Task<UploadPhotoResult> AddAsync(IFormFile file, string folderName = "");
        Task<string> DeletePhotoAsync(string id);
    }

    public class UploadPhotoResult
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
    }

}