using System.Collections.Generic;
using domain;

namespace application.cqrs.auditTrail.queries
{
    public class GetAuditTrailsResult
    {
        public List<AuditTrail> History { get; internal set; }
    }
}