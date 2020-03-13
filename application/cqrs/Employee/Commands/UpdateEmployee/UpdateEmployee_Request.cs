using application.cqrs._base;
using FluentValidation;
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
        public long ReportsTo { get; set; }
        public bool CanApprove { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateEmployee_Request_Validator : AbstractValidator<UpdateEmployee_Request>
    {
        public UpdateEmployee_Request_Validator()
        {
            RuleFor(i => i.EmployeeNumber).NotEmpty();
            RuleFor(i => i.FirstName).NotEmpty();
            RuleFor(i => i.LastName).NotEmpty();
            RuleFor(i => i.CompanyEmail).EmailAddress().NotEmpty();
            RuleFor(i => i.EmployeeID).NotEmpty();
        }
    }
}
