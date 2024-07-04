using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TripPhoto : Photo
    {
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }
    }
}