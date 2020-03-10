using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class BiologModel
    {
        [Required(AllowEmptyStrings =false)]
        public string EmployeeNumber { get; set; }

        [Required]
        public string Lat { get; set; }

        [Required]
        public string Long { get; set; }

        [Required(AllowEmptyStrings =false)]
        [MaxLength(3)]
        [MinLength(2)]
        public string InOrOut { get; set; }
    }

    public class LoginMode
    {
        [MaxLength(3)]
        [MinLength(2)]
        public string InOrOut { get; set; }
    }
}
