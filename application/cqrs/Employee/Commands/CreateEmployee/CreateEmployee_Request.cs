using application.cqrs._base;
using FluentValidation;
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
        public long ApproverID { get; set; }
        public bool CanApprove { get; set; }
    }

    public class CreateEmployee_Request_Validator : AbstractValidator<CreateEmployee_Request>
    {
        public CreateEmployee_Request_Validator()
        {
            RuleFor(i => i.EmployeeNumber).NotEmpty();
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            RuleFor(i => i.CompanyEmail).EmailAddress().NotEmpty();

        }
    }
}