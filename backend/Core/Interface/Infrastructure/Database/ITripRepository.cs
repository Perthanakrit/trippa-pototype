using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Core.Interface.Infrastructure.Database
{
    public interface ITripRepository : IBaseRepository<Trip>
    {
        Task<Trip> GetByIdCache(Guid id);
    }
}