using application.cqrs._base;

namespace application.cqrs.auditTrail
{
    public class CreateAuditTrailResponse : CommandResponseBase
    {
        public CreateAuditTrailResponse(string entity, object id) : base(entity, id)
        {
        }
    }
}