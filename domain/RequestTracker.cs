using System;
using System.Collections.Generic;

namespace domain
{
    public class RequestTracker : BaseAudit<long>
    {
        public RequestTracker()
        {
            ApproverList = new HashSet<ApprovalTracker>();
        }
        public Guid RequestorID { get; set; }
        public RequestType TypeOfRequest { get; set; }

        public Employee Requestor { get; set; }
        public bool IsCancelled { get; set; }
        public virtual ICollection<ApprovalTracker> ApproverList { get; }
    }

    public enum RequestType : long
    {
        Overtime = 1,
        Leave = 2,
        CertificateOfAttendance = 3,
        ScheduleAdjustment = 4,
        Undertime = 5,
        OfficialBusiness = 6
    }

}
