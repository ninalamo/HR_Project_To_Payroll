using System;

namespace domain
{
    public class RequestApprover : BaseAudit<long>
    {
        public Guid RequestorID { get; set; }
        public long ApproverID { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }

        public Employee Requestor { get; set; }
        public Approver Approver { get; set; }
    }

}
