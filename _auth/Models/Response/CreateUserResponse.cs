using System;

namespace auth.server.jwt.Models.Response
{
    public class CreateUserResponse : AppResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

    }
}
