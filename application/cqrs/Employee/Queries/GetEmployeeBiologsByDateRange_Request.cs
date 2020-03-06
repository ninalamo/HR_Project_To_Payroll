using MediatR;
using System;

namespace HR.Application.cqrs.Employee.Queries
{
    public class GetEmployeeBiologsByDateRange_Request : IRequest<GetEmployeeBiologsByDateRange_Response>
    {
        public DateTimeOffset Date1 { get; set; }
        public DateTimeOffset Date2 { get; set; }
        public Guid? EmployeeID { get; set; }
    }
}