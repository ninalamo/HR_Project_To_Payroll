using MediatR;

namespace application.cqrs.Employee.Commands
{
    public class CreateEmployeeRequest : IRequest<CreateEmployeeResponse>
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}