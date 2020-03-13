using application.cqrs._base;
using MediatR;

namespace HR.Application.cqrs.Request.Commands
{
    public class CancelOverTimeRequest_Request : CreditableBase, IRequest
    {
        public long OTRequestID { get; set; }
        public string RequestorEmail { get; set; }
        public string Note { get; set; }
    }
}