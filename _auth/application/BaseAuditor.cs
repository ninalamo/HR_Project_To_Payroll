using application.interfaces;
using domain;
using lib.common.interfaces;
using System;

namespace application
{
    public abstract class BaseAuditor : ITakeCredit
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

      
    }
}
