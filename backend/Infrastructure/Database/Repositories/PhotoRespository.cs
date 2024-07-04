using System.Linq.Expressions;
using Core.Interface.Infrastructure.Cloudinary;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Database.Repositories
{
    public class PhotoRespository<TEntity> : IPhotoRespository<TEntity> where TEntity : BaseEntity
    {
        private readonly DatabaseContext _db;

        public PhotoRespository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            // Add photo to cloudinary and save the url to the database 


            await _db.Set<TEntity>().AddAsync(entity);

            return await _db.SaveChangesAsync() > 0;
        }

        public Task<string> DeletePhotoAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}