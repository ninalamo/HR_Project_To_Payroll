using application.cqrs._base;
using MediatR;

namespace HR.Application.cqrs.Employee.Commands
{
    public class CreateEmployee_Request : CreditableBase, IRequest<CreateEmployee_Response>
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyEmail { get; set; }
        public string PersonalEmail { get; set; }
    }
}