using System;

namespace domain
{
    public class Approver : BaseAudit<long>
    {
        public Guid EmployeeID { get; set; }
        public int Level { get; set; }

        public Employee Employee { get; set; }
        public RequestType TypeOfRequest { get; set; }
    }

   

}
