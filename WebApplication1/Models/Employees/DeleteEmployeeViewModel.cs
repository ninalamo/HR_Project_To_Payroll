﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Employees
{
    public class DeleteEmployeeViewModel
    {

        [Required]
        public Guid EmployeeID { get; set; }
        [Required]
        [Display(Name = "Employee Number")]
        public string EmployeeNumber { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Company Email")]
        public string CompanyEmail { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Personal Email")]
        public string PersonalEmail { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
