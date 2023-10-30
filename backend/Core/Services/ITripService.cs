using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Core.Services
{
    public interface ITripService
    {

    }

    public class TripServiceInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class TripServiceResponse : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}