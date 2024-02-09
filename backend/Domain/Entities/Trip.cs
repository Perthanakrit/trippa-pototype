using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Trip : BaseEntity, IBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Landmark { get; set; }
        public string Duration { get; set; }
        public float Price { get; set; }
        public float Fee { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public string HostId { get; set; }
        public bool IsCustomTrip { get; set; }

        #region Relationships
        public Guid TypeOfTripId { get; set; } // Foreign key
        public TypeOfTrip TypeOfTrip { get; set; } // Navigation property
        public ICollection<TripAgenda> TripAgenda { get; set; } = new List<TripAgenda>(); // Navigation property
        public ICollection<TripAttendee> Attendee { get; set; } = new List<TripAttendee>(); // Navigation property
        #endregion
    }
}