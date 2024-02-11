using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
    }
}