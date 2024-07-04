using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Infrastructure.Database
{
    public interface IUserPhotoRespository : IBaseRepository<UserPhoto>
    {
        Task<bool> AddPhoto(IFormFile file);
        Task<string> DeletePhotoAsync(string id);
    }
}