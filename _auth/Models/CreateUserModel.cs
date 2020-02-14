using System;
using System.ComponentModel.DataAnnotations;

namespace auth.server.jwt.Models
{
    public class CreateUserModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        public Guid? AzureId { get; set; }
    }
}
