using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Employees
{
    public class ShortEmployeeViewModel
    {
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public Guid EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string ReportsTo { get; set; }
    }
}
