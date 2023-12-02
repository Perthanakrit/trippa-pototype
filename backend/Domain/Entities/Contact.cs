using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact : BaseEntity, IBaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Line { get; set; }

        public Guid UserId { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}