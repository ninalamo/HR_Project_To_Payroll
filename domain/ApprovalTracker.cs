using System;

namespace domain
{
    public class ApprovalTracker : BaseAudit<long>
    {
    
        public long? ApproverID { get; set; }
        public string ApproverEmail { get; set; }
        public string Note { get; set; }
        public bool? Status { get; set; }

      
        public Approver Approver { get; set; }
    }

}
