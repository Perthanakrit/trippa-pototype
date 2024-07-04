using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TripAgenda : TripSection
    {
        public Guid TripId { get; set; } // Foreign key
        public Trip Trip { get; set; } // Navigation property
    }
}