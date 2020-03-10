using application.cqrs._base;
using domain;
using MediatR;

namespace HR.Application.cqrs.Approver.Commands
{
    public class CreateApprover_Request : CreditableBase, IRequest
    {
        public string CompanyEmail { get; internal set; }
        public int Level { get; internal set; }
        public RequestType TypeOfRequest { get; internal set; }
    }
}