using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TripAgenda
    {
        public int Id { get; set; }
        public Guid TripId { get; set; } // Foreign key
        public Trip Trip { get; set; } // Navigation property
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly Time { get; set; }
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
    }
}