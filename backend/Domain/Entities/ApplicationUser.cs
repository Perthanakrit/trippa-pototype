using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        #region relationship 
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<TripAttendee> AttendeedTrips { get; set; }
        #endregion
    }
}