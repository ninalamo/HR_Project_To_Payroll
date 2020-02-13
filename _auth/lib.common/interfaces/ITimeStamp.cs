using System;
using System.Collections.Generic;
using System.Text;

namespace lib.common.interfaces
{
    public interface ITimeStamp
    {
        DateTimeOffset CreatedOn { get; set; }
        DateTimeOffset ModifiedOn { get; set; }
    }
}
