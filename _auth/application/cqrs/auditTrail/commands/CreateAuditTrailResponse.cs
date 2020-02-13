using application.cqrs._base;

namespace application.cqrs.auditTrail.commands
{
    public class CreateAuditTrailResponse : CommandResponseBase<long>
    {
        public CreateAuditTrailResponse(string entity, long id) : base(entity, id)
        {
        }
    }


}