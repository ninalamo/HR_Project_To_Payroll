using application.cqrs._base;
using domain;
using MediatR;

namespace HR.Application.cqrs.Approver.Commands
{
    public class CreateApprover_Request : CreditableBase, IRequest
    {
        public string CompanyEmail { get; set; }
        public int Level { get; set; }
        public RequestType TypeOfRequest { get; set; }
    }
}