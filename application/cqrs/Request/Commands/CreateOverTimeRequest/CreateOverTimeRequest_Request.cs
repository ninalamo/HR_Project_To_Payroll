using application.cqrs._base;
using MediatR;

namespace HR.Application.cqrs.Request.Commands
{
    public class CreateOverTimeRequest_Request : CreditableBase, IRequest
    {
        public string Details { get; set; }
        public string Requestor { get; set; }
        public string Supervisor { get; set; }
        public string FinalApprover { get; set; }
    }
}