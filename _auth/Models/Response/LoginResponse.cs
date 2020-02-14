using System;

namespace auth.server.jwt.Models.Response
{
    public class LoginResponse : AppResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}
