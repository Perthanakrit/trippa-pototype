using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Attendee
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsHost { get; set; }
        public DateTime AttendAt { get; set; }
        public DateTime? CancelAt { get; set; }
    }
}