using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Interface.Infrastructure.Cloudinary;
using Core.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.CloudinaryServices
{
    public class PhotoCloudinary : IPhotoCloudinary
    {
        private readonly Cloudinary _cloudinary;
        public PhotoCloudinary(IOptions<CloudinaySettings> config)
        {
            Account account = new Account
            {
                Cloud = config.Value.CloudName,
                ApiKey = config.Value.ApiKey,
                ApiSecret = config.Value.ApiSecrect
            };

            _cloudinary = new Cloudinary(account);

        }

        public async Task<UploadPhotoResult> AddAsync(IFormFile file)
        {
            if (file.Length <= 0) return default;

            using var stream = file.OpenReadStream();

            // filename grenerate from date time
            string fileName = $"{DateTime.Now:yyyyMMddHHmmssfff}{file.FileName}";

            ImageUploadParams uploadParams = new()
            {
                File = new FileDescription(fileName, stream),
                Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
            };

            ImageUploadResult uploadResult = await _cloudinary.UploadAsync(uploadParams);

            // throw new ArgumentException($"{JsonConvert.SerializeObject(uploadResult)}");

            if (uploadResult.Error != null) throw new Exception(uploadResult.Error.Message);

            return new UploadPhotoResult
            {
                Url = uploadResult.SecureUrl.AbsoluteUri,
                PublicId = uploadResult.PublicId
            };
        }

        public async Task<string> DeletePhotoAsync(string id)
        {
            DeletionParams deleteParams = new DeletionParams(id);
            DeletionResult result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : null;
        }
    }
}