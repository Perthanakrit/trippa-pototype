using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
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

        public async Task<TEntity> GetById<Tpop>(Guid id, Expression<Func<TEntity, Tpop>> include = null)
        {
            if (include != null)
            {
                return await _context.Set<TEntity>().Include(include).FirstOrDefaultAsync(e => e.Id == id);
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            // entity.UpdatedAt = DateTime.UtcNow;
            // entity.IsActive = false;
            _context.Remove(entity);
            return entity;
        }

        public async Task<T> SaveChangesAsync<T>()
        {
            return await (_context.SaveChangesAsync() as Task<T>);
        }

        public IQueryable<TEntity> GetByIdQueryable(Guid id, Expression<Func<TEntity, TEntity>> orderBy = null)
        {
            // if (orderBy != null)
            // {
            //     return _context.Set<TEntity>().Where(e => e.Id == id).OrderBy(orderBy);
            // }
            return _context.Set<TEntity>().Where(e => e.Id == id).AsQueryable();
        }

        public IQueryable<TEntity> GetAllQueryable()
        {
            return _context.Set<TEntity>().AsQueryable<TEntity>();
        }
    }
}