using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomTrip : BaseEntity, IBaseEntity
    {
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }
    }
}