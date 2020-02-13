using MediatR;

namespace application.cqrs.auditTrail.queries
{
    public class GetAuditTrailsQuery : IRequest<GetAuditTrailsResult>
    {
        public string Requestor { get; set; }
    }
}