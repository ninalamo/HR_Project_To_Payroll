using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Employees
{
    public class UpdateEmployeeViewModel
    {


        [Required]
        public Guid EmployeeID { get; set; }
        [Required]
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
        public string ModifiedBy { get; set; }
        public bool IsActive { get;  set; }
    }
}
