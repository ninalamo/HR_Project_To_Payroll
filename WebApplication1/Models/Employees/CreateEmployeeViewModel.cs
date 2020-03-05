using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Employees
{
    public class CreateEmployeeViewModel
    {
        [Required]
        [Display(Name ="Employee Number")]
        public string EmployeeNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string CompanyEmail { get; set; }
        [Required]
        [EmailAddress]
        public string PersonalEmail { get; set; }
        public string CreatedBy { get; set; }
    }
}
