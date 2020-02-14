using MediatR;
using System;

namespace HR.Application.cqrs.Employee.Commands
{
    public class DeleteEmployeeRequest : IRequest<Unit>
    {
        public Guid EmployeeID { get; set; }
    }
}
