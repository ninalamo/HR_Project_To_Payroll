using System;

namespace domain
{
    public class Employee : BaseAudit<Guid>
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string ReportsTo { get; set; }
    }

}
