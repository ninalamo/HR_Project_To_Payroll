using application.cqrs._base;
using MediatR;

namespace application.cqrs.auditTrail.commands
{
    public class CreateAuditTrailRequest :  CreditableBase, IRequest<CreateAuditTrailResponse>
    {
       
    }
}