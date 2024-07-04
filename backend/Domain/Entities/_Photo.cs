using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Photo : BaseEntity
    {
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}