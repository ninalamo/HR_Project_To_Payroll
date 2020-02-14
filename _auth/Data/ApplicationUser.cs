using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace auth.api.Data
{
    public class ApplicationUser : IdentityUser
    {
        public Guid? AzureId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public bool IsActive { get; set; }
    }

   
}