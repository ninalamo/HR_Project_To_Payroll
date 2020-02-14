using System.ComponentModel.DataAnnotations;

namespace auth.server.jwt.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Key { get; set; }
    }
}
