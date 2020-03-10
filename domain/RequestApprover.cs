using System;

namespace domain
{
    public class RequestApprover : BaseAudit<long>
    {
    
        public long ApproverID { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }

      
        public Approver Approver { get; set; }
    }

}
