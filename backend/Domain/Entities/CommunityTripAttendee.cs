using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommunityTripAttendee : Attendee
    {
        public Guid CommunityTripId { get; set; }
        public CommunityTrip CommunityTrip { get; set; }
    }
}