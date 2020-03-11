using application.cqrs._base;
using MediatR;
using System;

namespace HR.Application.cqrs.Request.Commands
{
    public class CreateOverTimeRequest_Request : CreditableBase, IRequest
    {
        public string Requestor { get; set; }
        public string Supervisor { get; set; }
        public string FinalApprover { get; set; }
        public string Classification { get; set; }
        public string Purpose { get; set; }
        public DateTimeOffset ShiftDate { get; set; }
        public DateTimeOffset TimeStart { get; set; }
        public DateTimeOffset TimeEnd { get; set; }
    }
}