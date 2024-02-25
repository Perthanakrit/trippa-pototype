using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        #region relationship 
        public Guid UserPhotoId { get; set; }
        public virtual UserPhoto Image { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<TripAttendee> AttendeedTrips { get; set; }
        #endregion
    }
}