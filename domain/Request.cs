using System;
using System.Collections.Generic;

namespace domain
{
    public class Request : BaseAudit<long>
    {
        public Request()
        {
            ApproverList = new HashSet<RequestApprover>();
        }
        public Guid RequestorID { get; set; }
        public string Details { get; set; }
        public RequestType TypeOfRequest { get; set; }

        public Employee Requestor { get; set; }
        public virtual ICollection<RequestApprover> ApproverList { get; }
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
