using application.cqrs._base;
using domain;
using MediatR;

namespace HR.Application.cqrs.Approver.Commands
{
    public class UpdateApprover_Request : CreditableBase, IRequest
    {
        public int Level { get; set; }
        public RequestType TypeOfRequest { get; set; }
        public bool IsActive { get; set; }
        public long ApproverID { get; set; }
    }
}