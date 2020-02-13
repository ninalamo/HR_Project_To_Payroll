using System;
using lib.common.interfaces;

namespace domain
{
    public class BaseAudit<Tidentity> : ITakeCredit, ITimeStamp, IActive, IHaveID<Tidentity> where Tidentity : struct
    {
        
        public bool IsActive { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Tidentity ID { get; set; }
    }

}
