using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommunityTripAppointment : TripSection
    {
        public Guid CommuTripId { get; set; }
        public CommunityTrip CommuTrip { get; set; }
    }
}