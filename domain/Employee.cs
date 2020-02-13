using System;

namespace domain
{
    public class Employee : BaseAudit<Guid>
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
