using System.ComponentModel.DataAnnotations;

namespace auth.server.jwt.Models
{
    public class PermissionDTO
    {
        [Required]
        public bool IsAllowed { get; set; }
        //[Required]
        //public string Controller { get; set; }
        //[Required]
        //public string Action { get; set; }

        [Required]
        public string AsClaim { get; set; }
    }
}
