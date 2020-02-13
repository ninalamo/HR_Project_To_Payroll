using System;
using System.Collections.Generic;
using System.Text;

namespace lib.common.interfaces
{
    public interface ITakeCredit
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
