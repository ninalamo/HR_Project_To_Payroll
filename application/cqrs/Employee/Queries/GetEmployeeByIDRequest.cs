using MediatR;
using System;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeByIDRequest : IRequest<GetEmployeeByIDResponse>
    {
        public Guid EmployeeID { get; set; }
    }
}