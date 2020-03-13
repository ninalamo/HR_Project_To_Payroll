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

    public enum ApproverTypeEnum : int
    {
        FirstLevelApprover = 1,
        SecondLevelApprover = 2,
        ThirdLevelApprover = 3,
        FinalApprover = 9999
    }

   

}
