using MediatR;
using System;

namespace HR.Application.cqrs.Request.Queries
{
    public class GetOverTimeRequests_Request : IRequest<GetOverTimeRequests_Response>
    {
        public Guid? EmployeeID { get; set; }
    }
}