using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomTrip : BaseEntity, IBaseEntity
    {
        //public Gu
        public string Name { get; set; }
        public string Description { get; set; }
        public string landmark { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}