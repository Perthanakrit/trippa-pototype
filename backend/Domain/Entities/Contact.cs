using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Contact
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
        [JsonIgnore]
        public ApplicationUser User { get; set; }
        public string Channel { get; set; }
        public string Name { get; set; }
    }
}