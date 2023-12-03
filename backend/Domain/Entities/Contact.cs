using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact : BaseEntity, IBaseEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Facebook { get; set; }
        public string Line { get; set; }

        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}