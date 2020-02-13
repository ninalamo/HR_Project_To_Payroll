using System;

namespace domain
{
    public class ConsolidatedBioLog : BaseAudit<long>
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public DateTimeOffset TimeIn { get; set; }
        public DateTimeOffset TimeOut { get; set; }
        public string Remarks { get; set; }

        public string OverridenBy { get; set; }
        public DateTimeOffset OverrideDate { get; set; }
        public string ReasonForOverride { get; set; }

        public Guid EmployeeID { get; set; }
        public Employee Employee { get; set; }
    }
}
