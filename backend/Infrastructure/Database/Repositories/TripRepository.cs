using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Interface.Infrastructure.Database;
using Domain.Entities;

namespace Infrastructure.Database.Repositories
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(DatabaseContext context) : base(context)
        {

        }
        // Inherit the methods from the base repository

    }
}