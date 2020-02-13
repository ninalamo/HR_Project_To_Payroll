using System;

namespace domain
{
    public class EmployeeSchedule : BaseAudit<long>
    {
        public Guid EmployeeID { get; set; }
        public long ShiftID { get; set; }


        public Employee Employee { get; set; }
        public Shift Shift { get; set; }
    }

}
