using System;
using System.Collections.Generic;
using System.Text;

namespace application.notifications.models
{
    public class Message
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        //public AuditTrail Audit { get; set; }
    }
}
