using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext _context;

        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = entity.CreatedAt;
            entity.IsActive = true;

            await _context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsActive = false;
            _context.Remove(entity);
            return entity;
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }


    }
}