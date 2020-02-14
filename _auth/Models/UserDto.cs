using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auth.server.jwt.Models
{
    public class UserDto
    {
        public string Email { get; set; }
        public Guid? AzureId { get; set; }
        public string Id { get; set; }
        public UserClaim[] Claims { get; set; }
    }

    public class UserClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
