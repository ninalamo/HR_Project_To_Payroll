using System;

namespace domain
{
    public class OverTimeRequest : BaseAudit<long>
    {
        public long TrackerID { get; set; }
        public Guid EmployeeID { get; set; }
        public DateTimeOffset TimeStart { get; set; }
        public DateTimeOffset TimeEnd { get; set; }
        public string Classification { get; set; }
        public DateTimeOffset ShiftDate { get; set; }
        public string Purpose { get; set; }

        public RequestTracker Tracker { get; set; }
  
    }

}
