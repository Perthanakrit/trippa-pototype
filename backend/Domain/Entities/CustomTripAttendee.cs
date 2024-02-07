using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomTripAttendee : Attendee
    {
        public CustomTrip CustomTrip { get; set; }
        public Guid CustomTripId { get; set; }
    }
}