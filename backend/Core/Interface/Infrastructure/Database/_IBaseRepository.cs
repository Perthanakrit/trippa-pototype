using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Core.Interface.Infrastructure.Database
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> GetById<TPop>(Guid id, Expression<Func<TEntity, TPop>> include = null);
        IQueryable<TEntity> GetByIdQueryable(Guid id, Expression<Func<TEntity, TEntity>> orderBy = null);
        Task<List<TEntity>> GetAll();
        IQueryable<TEntity> GetAllQueryable();
        Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        Task<T> SaveChangesAsync<T>();
    }
}