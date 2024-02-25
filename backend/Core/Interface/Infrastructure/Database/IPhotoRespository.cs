using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Interface.Infrastructure.Database
{
    public interface IPhotoRespository<T> where T : IBaseEntity
    {
        Task<bool> AddAsync(T entity);
        Task<string> DeletePhotoAsync(string id);
    }
}