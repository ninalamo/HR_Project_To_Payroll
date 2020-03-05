using application.cqrs._base;
using MediatR;
using System;

namespace HR.Application.cqrs.Employee.Commands
{
    public class UpdateEmployee_Request : CreditableBase, IRequest<UpdateEmployee_Response>
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
        public Guid PersonID { get; set; }
        public bool IsActive { get; set; }
    }
}
