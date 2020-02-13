using System;
using System.Collections.Generic;
using System.Text;

namespace domain
{
    public class BioLog : BaseAudit<long>
    {
        public Guid EmployeeID { get; set; }
        public BiologType LogType { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Locaton { get; set; }
        public DateTimeOffset Time { get; set; }
    }

    public enum BiologType : int
    {
        IN = 1,
        OUT = 2,
    }
}
